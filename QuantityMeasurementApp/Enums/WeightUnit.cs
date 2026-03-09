namespace QuantityMeasurementApp.Enums
{
    public enum WeightUnit
    {
        KILOGRAM,
        GRAM,
        POUND,
        TONNE
    }

    public static class WeightUnitExtensions
    {
        public static double GetConversionFactor(this WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.KILOGRAM => 1.0,
                WeightUnit.GRAM => 0.001,
                WeightUnit.POUND => 0.453592,
                _ => throw new ArgumentException("Invalid unit")
            };
        }

        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            double result = value * unit.GetConversionFactor();
            return Math.Round(result, 2);
        }

        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            double result = baseValue / unit.GetConversionFactor();
            return Math.Round(result, 2);
        }
    }
}