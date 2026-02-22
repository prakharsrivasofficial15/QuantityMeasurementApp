using System;

namespace QuantityMeasurementApp.UC2
{
    public class Feet
    {
        public double Value { get; }

        public Feet(double value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null || obj.GetType() != typeof(Feet))
                return false;

            var other = (Feet)obj;

            return Value.CompareTo(other.Value) == 0;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}