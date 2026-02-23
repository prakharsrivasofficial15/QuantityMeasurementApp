using System;

namespace QuantityMeasurementApp.UC3
{
    public enum LengthUnit
    {
        Feet,
        Inches,
        Yards,
        Centimeters
    }

    public static class LengthUnitExtensions
    {
        public static double ToInchesFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 12.0,
                LengthUnit.Inches => 1.0,
                LengthUnit.Yards => 36.0,
                LengthUnit.Centimeters => 0.393701,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }
    }

    public class Length : IEquatable<Length>
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

        private const double Tolerance = 0.0001;

        public Length(double value, LengthUnit unit)
        {
            _value = value;
            _unit = unit;
        }

        private double ConvertToBaseUnit()
        {
            return _value * _unit.ToInchesFactor();
        }

        public bool Equals(Length? other)
        {
            if (other is null)
                return false;

            return Math.Abs(ConvertToBaseUnit() - other.ConvertToBaseUnit()) < Tolerance;
        }

        public override bool Equals(object? obj)
        {
            return obj is Length other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Math.Round(ConvertToBaseUnit(), 4).GetHashCode();
        }
    }
}