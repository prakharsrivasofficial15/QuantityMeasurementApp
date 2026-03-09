namespace QuantityMeasurementApp.Enums
{
    public enum WeightUnit
    {
        MILLIGRAM,
        GRAM,
        KILOGRAM,
        POUND,
        TONNE
    }

    public static class WeightUnitExtensions
    {
        public static double GetConversionFactor(this WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.MILLIGRAM => 0.001,
                WeightUnit.GRAM => 1.0,
                WeightUnit.KILOGRAM => 1000.0,
                WeightUnit.POUND => 453.592,
                WeightUnit.TONNE => 1000000.0,
                _ => throw new ArgumentException("Invalid unit")
            };
        }

        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            return Math.Round(value * unit.GetConversionFactor(), 2);
        }

        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            return Math.Round(baseValue / unit.GetConversionFactor(), 2);
        }

        public static string GetUnitName(this WeightUnit unit)
        {
            return unit.ToString();
        }
    }
}