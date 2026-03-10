namespace QuantityMeasurementApp.Interfaces
{
    public delegate bool SupportsArithmetic();

    // public interface IMeasurable
    // {
    //     double GetConversionFactor();

    //     double ConvertToBaseUnit(double value);

    //     double ConvertFromBaseUnit(double baseValue);

    //     string GetUnitName();
    // }
    public interface IMeasurable
    {
        string GetUnitName();

        double GetConversionFactor();

        double ConvertToBaseUnit(double value);

        double ConvertFromBaseUnit(double baseValue);

        // Lambda default → arithmetic supported
        SupportsArithmetic SupportsArithmetic => () => true;

        // Default method
        bool SupportsArithmeticOperation()
        {
            return SupportsArithmetic();
        }

        // Default validation method
        void ValidateOperationSupport(string operation)
        {
            // Default = allow operations
        }
    }
}