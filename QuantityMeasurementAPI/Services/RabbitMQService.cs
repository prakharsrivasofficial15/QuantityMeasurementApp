using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace QuantityMeasurementAPI.Services
{
    public class RabbitMQService : IMessageQueueService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;
        private readonly ILogger<RabbitMQService> _logger;

        public RabbitMQService(IConfiguration configuration, ILogger<RabbitMQService> logger)
        {
            _logger = logger;
            _queueName = configuration["RabbitMQ:QueueName"] ?? "measurement_events";
            
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:HostName"] ?? "localhost",
                Port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672"),
                UserName = configuration["RabbitMQ:UserName"] ?? "guest",
                Password = configuration["RabbitMQ:Password"] ?? "guest"
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false);
                _logger.LogInformation("RabbitMQ connection established");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to RabbitMQ");
                throw;
            }
        }

        public void PublishMeasurementEvent(string operation, object data)
        {
            try
            {
                var message = new
                {
                    Operation = operation,
                    Data = data,
                    Timestamp = DateTime.UtcNow
                };
                
                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);
                
                _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
                _logger.LogInformation("Published measurement event: {Operation}", operation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish message to RabbitMQ");
            }
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}