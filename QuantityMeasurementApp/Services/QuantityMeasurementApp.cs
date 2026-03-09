using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Enums;

namespace QuantityMeasurementApp.Services
{
    public static class QuantityMeasurementApp
    {
        public static bool DemonstrateLengthEquality(Length l1, Length l2)
        {
            return l1.Equals(l2);
        }

        public static Length DemonstrateLengthConversion(
            double value,
            LengthUnit fromUnit,
            LengthUnit toUnit)
        {
            var length = new Length(value, fromUnit);
            return length.ConvertTo(toUnit);
        }

        public static Length DemonstrateLengthConversion(
            Length length,
            LengthUnit toUnit)
        {
            return length.ConvertTo(toUnit);
        }

        public static Length DemonstrateLengthAddition(Length length1, Length length2)
        {
            return length1.Add(length2);
        }
    }
}