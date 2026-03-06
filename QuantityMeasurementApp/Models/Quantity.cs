namespace QuantityMeasurementApp.Models
{
    public class Quantity
    {
        public double Value { get; }

        public Quantity(double value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj == null || obj.GetType() != typeof(Quantity))
                return false;

            Quantity other = (Quantity)obj;

            return Value.CompareTo(other.Value) == 0;
        }
        
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}