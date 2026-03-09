using QuantityMeasurementApp.Enums;

namespace QuantityMeasurementApp.Models
{
    public class Weight
    {
        private readonly double value;
        private readonly WeightUnit unit;

        public Weight(double value, WeightUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            if (unit == null)
                throw new ArgumentException("Unit cannot be null");

            this.value = value;
            this.unit = unit;
        }

        public double GetValue() => value;
        public WeightUnit GetUnit() => unit;

        private double ConvertToBaseUnit()
        {
            return unit.ConvertToBaseUnit(value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj == null || obj.GetType() != typeof(Weight))
                return false;

            Weight other = (Weight)obj;

            return ConvertToBaseUnit() == other.ConvertToBaseUnit();
        }

        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }

        public Weight ConvertTo(WeightUnit targetUnit)
        {
            double baseValue = ConvertToBaseUnit();
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Weight(converted, targetUnit);
        }

        public Weight Add(Weight other)
        {
            double base1 = this.ConvertToBaseUnit();
            double base2 = other.ConvertToBaseUnit();

            double sumBase = base1 + base2;

            double resultValue = unit.ConvertFromBaseUnit(sumBase);

            return new Weight(resultValue, unit);
        }

        public Weight Add(Weight other, WeightUnit targetUnit)
        {
            double base1 = this.ConvertToBaseUnit();
            double base2 = other.ConvertToBaseUnit();

            double sumBase = base1 + base2;

            double resultValue = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Weight(resultValue, targetUnit);
        }

        public override string ToString()
        {
            return $"{value} {unit}";
        }
    }
}