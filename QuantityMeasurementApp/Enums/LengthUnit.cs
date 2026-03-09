using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp.Enums
{
    public enum LengthUnit
    {
        FEET,
        INCHES,
        YARDS,
        CENTIMETERS
    }

    public static class LengthUnitExtensions
    {
        public static double GetConversionFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 12.0,
                LengthUnit.INCHES => 1.0,
                LengthUnit.YARDS => 36.0,
                LengthUnit.CENTIMETERS => 0.393701,
                _ => throw new ArgumentException("Invalid unit")
            };
        }

        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            return Math.Round(value * unit.GetConversionFactor(), 2);
        }

        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            return Math.Round(baseValue / unit.GetConversionFactor(), 2);
        }

        public static string GetUnitName(this LengthUnit unit)
        {
            return unit.ToString();
        }
    }
}