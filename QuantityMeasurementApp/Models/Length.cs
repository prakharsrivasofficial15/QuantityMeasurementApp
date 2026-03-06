using QuantityMeasurementApp.Enums;

namespace QuantityMeasurementApp.Models
{
    public class Length
    {
        private readonly double value;
        private readonly LengthUnit unit;

        public Length(double value, LengthUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        private double ConvertToBaseUnit()
        {
            return value * (double)unit;
        }

        public bool Compare(Length otherLength)
        {
            return ConvertToBaseUnit() == otherLength.ConvertToBaseUnit();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj == null || obj.GetType() != typeof(Length))
                return false;

            Length other = (Length)obj;

            return Compare(other);
        }

        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }
    }
}