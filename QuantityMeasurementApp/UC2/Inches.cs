using System;

namespace QuantityMeasurementApp.UC2
{
    public class Inches
    {
        public double Value { get; }

        public Inches(double value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null || obj.GetType() != typeof(Inches))
                return false;

            var other = (Inches)obj;

            return Value.CompareTo(other.Value) == 0;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}