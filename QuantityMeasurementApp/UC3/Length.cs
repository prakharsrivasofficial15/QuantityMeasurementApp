using System;

namespace QuantityMeasurementApp.UC3
{
    public enum LengthUnit
    {
        Feet,
        Inches
    }

    public static class LengthUnitExtensions
    {
        public static double ToInchesFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 12.0,
                LengthUnit.Inches => 1.0,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }
    }

    public class Length : IEquatable<Length>
    {
        private readonly double _value;
        private readonly LengthUnit _unit;

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

            return ConvertToBaseUnit()
                   .CompareTo(other.ConvertToBaseUnit()) == 0;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return obj is Length other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }
    }
}