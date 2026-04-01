namespace QuantityMeasurementAPI.Services
{
    public interface IMessageQueueService
    {
        void PublishMeasurementEvent(string operation, object data);
    }
}