using QuantityMeasurementApp.Enums;

namespace QuantityMeasurementApp.Models
{
    public class Quantity
    {
        public double Value { get; }
        public UnitType Unit { get; }

        public Quantity(double value, UnitType unit)
        {
            Value = value;
            Unit = unit;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj == null || obj.GetType() != typeof(Quantity))
                return false;

            Quantity other = (Quantity)obj;

            if (Unit != other.Unit)
                return false;

            return Value.CompareTo(other.Value) == 0;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Unit);
        }
    }
}