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
        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            double result = unit switch
            {
                LengthUnit.FEET => value,
                LengthUnit.INCHES => value / 12,
                LengthUnit.YARDS => value * 3,
                LengthUnit.CENTIMETERS => value / 30.48,
                _ => throw new ArgumentException("Invalid unit")
            };

            return Math.Round(result, 2);
        }

        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            double result = unit switch
            {
                LengthUnit.FEET => baseValue,
                LengthUnit.INCHES => baseValue * 12,
                LengthUnit.YARDS => baseValue / 3,
                LengthUnit.CENTIMETERS => baseValue * 30.48,
                _ => throw new ArgumentException("Invalid unit")
            };

            return Math.Round(result, 2);
        }
    }
}