using BusinessLayer.Interfaces;
using BusinessLayer.Exceptions;
using ModelLayer.DTOs;
using System;

namespace QuantityMeasurementApp.Controllers
{
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService _service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        #region Demonstration Methods

        public void DemonstrateComparison(MeasurementRequest request1, MeasurementRequest request2)
        {
            try
            {
                Console.WriteLine($"Comparing: {request1.Value} {request1.Unit} vs {request2.Value} {request2.Unit}");
                var record = _service.Compare(request1, request2);
                
                if (record.Result is MeasurementRequest resultDto && resultDto.Value == 1)
                    Console.WriteLine($"Result: Equal ✓");
                else if (record.Result is MeasurementRequest resultDto2 && resultDto2.Value == 0)
                    Console.WriteLine($"Result: Not Equal ✗");
                else
                    Console.WriteLine($"Result: {record.Result}");
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error during comparison: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        public void DemonstrateConversion(MeasurementRequest request, string targetUnit)
        {
            try
            {
                Console.WriteLine($"Converting: {request.Value} {request.Unit} to {targetUnit}");
                var record = _service.Convert(request, targetUnit);
                
                if (record.Result is MeasurementRequest resultDto)
                {
                    Console.WriteLine($"Result: {resultDto.Value} {resultDto.Unit}");
                }
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error during conversion: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        public void DemonstrateAddition(MeasurementRequest request1, MeasurementRequest request2)
        {
            try
            {
                Console.WriteLine($"Adding: {request1.Value} {request1.Unit} + {request2.Value} {request2.Unit}");
                var record = _service.Add(request1, request2);
                
                if (record.Result is MeasurementRequest resultDto)
                {
                    Console.WriteLine($"Result: {resultDto.Value} {resultDto.Unit}");
                }
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error during addition: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        public void DemonstrateSubtraction(MeasurementRequest request1, MeasurementRequest request2)
        {
            try
            {
                Console.WriteLine($"Subtracting: {request1.Value} {request1.Unit} - {request2.Value} {request2.Unit}");
                var record = _service.Subtract(request1, request2);
                
                if (record.Result is MeasurementRequest resultDto)
                {
                    Console.WriteLine($"Result: {resultDto.Value} {resultDto.Unit}");
                }
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error during subtraction: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        public void DemonstrateDivision(MeasurementRequest request1, MeasurementRequest request2)
        {
            try
            {
                Console.WriteLine($"Dividing: {request1.Value} {request1.Unit} ÷ {request2.Value} {request2.Unit}");
                var record = _service.Divide(request1, request2);
                
                if (record.Result is MeasurementRequest resultDto)
                {
                    Console.WriteLine($"Result: {resultDto.Value} (dimensionless)");
                }
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error during division: {ex.Message}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine($"Error: Division by zero is not allowed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        #endregion

        #region Helper Display Methods

        public void DisplayResult(MeasurementRequest result, string operationName = "Operation")
        {
            if (result == null)
            {
                Console.WriteLine("No result to display");
                return;
            }

            Console.WriteLine($"\n{operationName} Result:");
            Console.WriteLine($"Value: {result.Value}");
            Console.WriteLine($"Unit: {result.Unit}");
            Console.WriteLine($"Type: {result.Type}");
        }

        public void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError: {message}");
            Console.ResetColor();
        }

        public void DisplaySuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nSuccess: {message}");
            Console.ResetColor();
        }

        public void DisplayWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nWarning: {message}");
            Console.ResetColor();
        }

        #endregion
    }
}