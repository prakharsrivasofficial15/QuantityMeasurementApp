using System;

namespace QuantityMeasurementApp.UC5
{
    public static class QuantityMeasurementService
    {
        public static double ConvertUnits(
            double value,
            LengthUnit source,
            LengthUnit target)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value.");

            // Convert to base unit (inches)
            double valueInInches = value * source.ToInchesFactor();

            // Convert from inches to target
            return valueInInches / target.ToInchesFactor();
        }
    }
}