using QuantityMeasurementApp.Enums;

namespace QuantityMeasurementApp.Models
{
    public class Length
    {
        private readonly double value;
        private readonly LengthUnit unit;

        public Length(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            this.value = value;
            this.unit = unit;
        }

        private double ConvertToBaseUnit()
        {
            return unit.ConvertToBaseUnit(value);
        }

        private bool Compare(Length other)
        {
            return ConvertToBaseUnit() == other.ConvertToBaseUnit();
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

        public Length ConvertTo(LengthUnit targetUnit)
        {
            double baseValue = ConvertToBaseUnit();
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Length(converted, targetUnit);
        }

        public Length Add(Length other)
        {
            double sumBase = ConvertToBaseUnit() + other.ConvertToBaseUnit();
            double result = unit.ConvertFromBaseUnit(sumBase);

            return new Length(result, unit);
        }

        public Length Add(Length other, LengthUnit targetUnit)
        {
            double sumBase = ConvertToBaseUnit() + other.ConvertToBaseUnit();
            double result = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Length(result, targetUnit);
        }
    }
}