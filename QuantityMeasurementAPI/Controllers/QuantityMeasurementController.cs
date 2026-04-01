using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuantityMeasurementAPI.DTOs;
using QuantityMeasurementAPI.Exceptions;
using QuantityMeasurementAPI.Models;
using QuantityMeasurementAPI.Services;
using BusinessLayer.Interfaces;
using ModelLayer.DTOs;
using System.Security.Claims;

namespace QuantityMeasurementAPI.Controllers
{
    /// <summary>
    /// Controller for handling quantity measurement operations like conversion, comparison, and arithmetic operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuantityMeasurementController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;
        private readonly IMemoryCache _cache;
        private readonly ILogger<QuantityMeasurementController> _logger;
        private readonly IMessageQueueService? _messageQueue;

        /// <summary>
        /// Initializes a new instance of the QuantityMeasurementController.
        /// </summary>
        /// <param name="service">The quantity measurement service.</param>
        /// <param name="cache">The memory cache for caching results.</param>
        /// <param name="logger">The logger for logging operations.</param>
        /// <param name="messageQueue">Optional message queue service for publishing events.</param>
        public QuantityMeasurementController(
            IQuantityMeasurementService service,
            IMemoryCache cache,
            ILogger<QuantityMeasurementController> logger,
            IMessageQueueService? messageQueue = null) 
        {
            _service = service;
            _cache = cache;
            _logger = logger;
            _messageQueue = messageQueue;
        }

        /// <summary>
        /// Publishes a measurement event to the message queue for logging and monitoring.
        /// </summary>
        /// <param name="operation">The operation type (e.g., COMPARE, CONVERT).</param>
        /// <param name="data">The data associated with the operation.</param>
        /// <param name="success">Whether the operation was successful.</param>
        /// <param name="error">Optional error message if the operation failed.</param>
        private void PublishMeasurementEvent(string operation, object data, bool success, string? error = null)
        {
            try
            {
                if (_messageQueue == null) return;
                
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var username = User.Identity?.Name;
                
                var measurementEvent = new MeasurementEvent
                {
                    Operation = operation,
                    Data = data,
                    UserId = userId,
                    Username = username,
                    Timestamp = DateTime.UtcNow,
                    Success = success,
                    Error = error
                };
                
                _messageQueue.PublishMeasurementEvent(operation, measurementEvent);
                _logger.LogDebug("Published {Operation} event to RabbitMQ", operation);
            }
            catch (Exception ex)
            {
                // Don't fail the main operation if RabbitMQ fails
                _logger.LogWarning(ex, "Failed to publish event to RabbitMQ for operation {Operation}", operation);
            }
        }

        /// <summary>
        /// Compares two quantities to check if they are equal.
        /// </summary>
        /// <param name="request">The comparison request containing two quantities.</param>
        /// <returns>A response indicating whether the quantities are equal.</returns>
        [HttpPost("compare")]
        public async Task<IActionResult> Compare([FromBody] CompareRequest request)
        {
            MeasurementResponse? response = null;
            bool success = true;
            string? error = null;
            
            try
            {
                _logger.LogInformation("Comparing quantities: {Value1} {Unit1} vs {Value2} {Unit2}",
                    request.Quantity1.Value, request.Quantity1.Unit,
                    request.Quantity2.Value, request.Quantity2.Unit);

                if (request.Quantity1.Type != request.Quantity2.Type)
                {
                    throw new BusinessException($"Cannot compare different measurement types: {request.Quantity1.Type} and {request.Quantity2.Type}");
                }

                var req1 = new MeasurementRequest
                {
                    Value = request.Quantity1.Value,
                    Unit = request.Quantity1.Unit,
                    Type = request.Quantity1.Type
                };
                
                var req2 = new MeasurementRequest
                {
                    Value = request.Quantity2.Value,
                    Unit = request.Quantity2.Unit,
                    Type = request.Quantity2.Type
                };

                var record = _service.Compare(req1, req2);
                
                bool isEqual = false;
                if (record.Result is MeasurementRequest resultDto)
                {
                    isEqual = resultDto.Value == 1;
                }

                response = new MeasurementResponse
                {
                    Value = isEqual ? 1 : 0,
                    Unit = "BOOLEAN",
                    Type = "RESULT",
                    IsEqual = isEqual,
                    Success = true
                };
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                success = false;
                error = ex.Message;
                _logger.LogError(ex, "Error comparing quantities");
                throw;
            }
            finally
            {
                // Publish event to RabbitMQ
                PublishMeasurementEvent("COMPARE", new 
                { 
                    Request = request, 
                    Response = response 
                }, success, error);
            }
        }

        /// <summary>
        /// Converts a quantity from one unit to another.
        /// </summary>
        /// <param name="request">The conversion request containing the quantity and target unit.</param>
        /// <returns>The converted quantity in the target unit.</returns>
        [HttpPost("convert")]
        public async Task<IActionResult> Convert([FromBody] ConvertRequest request)
        {
            MeasurementResponse? response = null;
            bool success = true;
            string? error = null;
            
            try
            {
                _logger.LogInformation("Converting {Value} {Unit} to {TargetUnit}",
                    request.Quantity.Value, request.Quantity.Unit, request.TargetUnit);

                var cacheKey = $"convert_{request.Quantity.Value}_{request.Quantity.Unit}_{request.TargetUnit}_{request.Quantity.Type}";
                
                if (_cache.TryGetValue(cacheKey, out MeasurementResponse? cachedResponse))
                {
                    _logger.LogInformation("Returning cached result for {CacheKey}", cacheKey);
                    response = cachedResponse;
                    return Ok(response);
                }

                var req = new MeasurementRequest
                {
                    Value = request.Quantity.Value,
                    Unit = request.Quantity.Unit,
                    Type = request.Quantity.Type
                };

                var record = _service.Convert(req, request.TargetUnit);
                
                if (record.HasError)
                {
                    throw new BusinessException(record.ErrorMessage ?? "Conversion failed");
                }
                
                if (record.Result is MeasurementRequest resultDto)
                {
                    response = new MeasurementResponse
                    {
                        Value = resultDto.Value,
                        Unit = resultDto.Unit,
                        Type = resultDto.Type,
                        Success = true
                    };
                }
                else
                {
                    throw new BusinessException("Conversion failed - invalid result");
                }

                _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                success = false;
                error = ex.Message;
                _logger.LogError(ex, "Error converting quantity");
                throw;
            }
            finally
            {
                PublishMeasurementEvent("CONVERT", new 
                { 
                    Request = request, 
                    Response = response 
                }, success, error);
            }
        }

        /// <summary>
        /// Adds two quantities of the same type.
        /// </summary>
        /// <param name="request">The arithmetic request containing two quantities to add.</param>
        /// <returns>The sum of the two quantities.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ArithmeticRequest request)
        {
            MeasurementResponse? response = null;
            bool success = true;
            string? error = null;
            
            try
            {
                _logger.LogInformation("Adding {Value1} {Unit1} + {Value2} {Unit2}",
                    request.Quantity1.Value, request.Quantity1.Unit,
                    request.Quantity2.Value, request.Quantity2.Unit);

                if (request.Quantity1.Type != request.Quantity2.Type)
                {
                    throw new BusinessException($"Cannot add different measurement types: {request.Quantity1.Type} and {request.Quantity2.Type}");
                }

                var req1 = new MeasurementRequest
                {
                    Value = request.Quantity1.Value,
                    Unit = request.Quantity1.Unit,
                    Type = request.Quantity1.Type
                };
                
                var req2 = new MeasurementRequest
                {
                    Value = request.Quantity2.Value,
                    Unit = request.Quantity2.Unit,
                    Type = request.Quantity2.Type
                };

                var record = _service.Add(req1, req2);
                
                if (record.HasError)
                {
                    throw new BusinessException(record.ErrorMessage ?? "Addition failed");
                }
                
                if (record.Result is not MeasurementRequest resultDto)
                {
                    throw new BusinessException("Addition failed - invalid result");
                }

                response = new MeasurementResponse
                {
                    Value = resultDto.Value,
                    Unit = resultDto.Unit,
                    Type = resultDto.Type,
                    Success = true
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                success = false;
                error = ex.Message;
                _logger.LogError(ex, "Error adding quantities");
                throw;
            }
            finally
            {
                PublishMeasurementEvent("ADD", new 
                { 
                    Request = request, 
                    Response = response 
                }, success, error);
            }
        }

        /// <summary>
        /// Subtracts one quantity from another of the same type.
        /// </summary>
        /// <param name="request">The arithmetic request containing two quantities to subtract.</param>
        /// <returns>The difference of the two quantities.</returns>
        [HttpPost("subtract")]
        public async Task<IActionResult> Subtract([FromBody] ArithmeticRequest request)
        {
            MeasurementResponse? response = null;
            bool success = true;
            string? error = null;
            
            try
            {
                _logger.LogInformation("Subtracting {Value1} {Unit1} - {Value2} {Unit2}",
                    request.Quantity1.Value, request.Quantity1.Unit,
                    request.Quantity2.Value, request.Quantity2.Unit);

                if (request.Quantity1.Type != request.Quantity2.Type)
                {
                    throw new BusinessException($"Cannot subtract different measurement types: {request.Quantity1.Type} and {request.Quantity2.Type}");
                }

                var req1 = new MeasurementRequest
                {
                    Value = request.Quantity1.Value,
                    Unit = request.Quantity1.Unit,
                    Type = request.Quantity1.Type
                };
                
                var req2 = new MeasurementRequest
                {
                    Value = request.Quantity2.Value,
                    Unit = request.Quantity2.Unit,
                    Type = request.Quantity2.Type
                };

                var record = _service.Subtract(req1, req2);
                
                if (record.HasError)
                {
                    throw new BusinessException(record.ErrorMessage ?? "Subtraction failed");
                }
                
                if (record.Result is not MeasurementRequest resultDto)
                {
                    throw new BusinessException("Subtraction failed - invalid result");
                }

                response = new MeasurementResponse
                {
                    Value = resultDto.Value,
                    Unit = resultDto.Unit,
                    Type = resultDto.Type,
                    Success = true
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                success = false;
                error = ex.Message;
                _logger.LogError(ex, "Error subtracting quantities");
                throw;
            }
            finally
            {
                PublishMeasurementEvent("SUBTRACT", new 
                { 
                    Request = request, 
                    Response = response 
                }, success, error);
            }
        }

        /// <summary>
        /// Divides one quantity by another of the same type.
        /// </summary>
        /// <param name="request">The arithmetic request containing two quantities to divide.</param>
        /// <returns>The quotient of the two quantities as a scalar value.</returns>
        [HttpPost("divide")]
        public async Task<IActionResult> Divide([FromBody] ArithmeticRequest request)
        {
            MeasurementResponse? response = null;
            bool success = true;
            string? error = null;
            
            try
            {
                _logger.LogInformation("Dividing {Value1} {Unit1} ÷ {Value2} {Unit2}",
                    request.Quantity1.Value, request.Quantity1.Unit,
                    request.Quantity2.Value, request.Quantity2.Unit);

                if (request.Quantity1.Type != request.Quantity2.Type)
                {
                    throw new BusinessException($"Cannot divide different measurement types: {request.Quantity1.Type} and {request.Quantity2.Type}");
                }

                var req1 = new MeasurementRequest
                {
                    Value = request.Quantity1.Value,
                    Unit = request.Quantity1.Unit,
                    Type = request.Quantity1.Type
                };
                
                var req2 = new MeasurementRequest
                {
                    Value = request.Quantity2.Value,
                    Unit = request.Quantity2.Unit,
                    Type = request.Quantity2.Type
                };

                var record = _service.Divide(req1, req2);
                
                if (record.HasError)
                {
                    throw new BusinessException(record.ErrorMessage ?? "Division failed");
                }
                
                if (record.Result is MeasurementRequest resultDto)
                {
                    response = new MeasurementResponse
                    {
                        Value = resultDto.Value,
                        Unit = "SCALAR",
                        Type = "RESULT",
                        Success = true
                    };
                }
                else
                {
                    throw new BusinessException("Division failed - invalid result");
                }

                return Ok(response);
            }
            catch (DivideByZeroException ex)
            {
                success = false;
                error = "Division by zero is not allowed";
                _logger.LogError(ex, "Division by zero attempted");
                throw new BusinessException(error);
            }
            catch (Exception ex)
            {
                success = false;
                error = ex.Message;
                _logger.LogError(ex, "Error dividing quantities");
                throw;
            }
            finally
            {
                PublishMeasurementEvent("DIVIDE", new 
                { 
                    Request = request, 
                    Response = response 
                }, success, error);
            }
        }
    }
}