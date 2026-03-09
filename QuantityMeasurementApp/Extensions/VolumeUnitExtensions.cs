using QuantityMeasurementApp.Enums;

namespace QuantityMeasurementApp.Extensions
{
    public static class VolumeUnitExtensions
    {

        public static double ConvertToBaseUnit(this VolumeUnit unit, double value)
        {
            return unit switch
            {
                VolumeUnit.MILLILITRE => value,
                VolumeUnit.LITRE => value * 1000,
                VolumeUnit.GALLON => value * 3785.41,
                _ => throw new ArgumentException("Unsupported volume unit")
            };
        }

        public static double ConvertFromBaseUnit(this VolumeUnit unit, double baseValue)
        {
            return unit switch
            {
                VolumeUnit.MILLILITRE => baseValue,
                VolumeUnit.LITRE => baseValue / 1000,
                VolumeUnit.GALLON => baseValue / 3785.41,
                _ => throw new ArgumentException("Unsupported volume unit")
            };
        }

        public static string GetUnitName(this VolumeUnit unit)
        {
            return unit.ToString();
        }
    }
}