using QuantityMeasurementApp.Enums;
using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp.Models
{
    public class Quantity<U>
    {
        private readonly double value;
        private readonly U unit;

        public Quantity(double value, U unit)
        {
            if (unit == null)
                throw new ArgumentException("Unit cannot be null");

            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid value");

            this.value = value;
            this.unit = unit;
        }

        public double GetValue()
        {
            return value;
        }

        public U GetUnit()
        {
            return unit;
        }

        private double ConvertToBase()
        {
            if (unit is LengthUnit lu)
                return lu.ConvertToBaseUnit(value);

            if (unit is WeightUnit wu)
                return wu.ConvertToBaseUnit(value);

            throw new ArgumentException("Unsupported unit type");
        }

        public double ConvertTo(U targetUnit)
        {
            double baseValue;

            if (unit is LengthUnit lu)
                baseValue = lu.ConvertToBaseUnit(value);
            else if (unit is WeightUnit wu)
                baseValue = wu.ConvertToBaseUnit(value);
            else
                throw new ArgumentException("Unsupported unit type");

            if (targetUnit is LengthUnit tlu)
                return tlu.ConvertFromBaseUnit(baseValue);

            if (targetUnit is WeightUnit twu)
                return twu.ConvertFromBaseUnit(baseValue);

            throw new ArgumentException("Unsupported unit type");
        }

        public Quantity<U> Add(Quantity<U> other)
        {
            dynamic source = unit;

            double base1 = source.ConvertToBaseUnit(value);
            double base2 = source.ConvertToBaseUnit(other.value);

            double sum = base1 + base2;

            double result = source.ConvertFromBaseUnit(sum);

            return new Quantity<U>(result, unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            dynamic source = unit;
            dynamic target = targetUnit;

            double base1 = source.ConvertToBaseUnit(value);
            double base2 = source.ConvertToBaseUnit(other.value);

            double sum = base1 + base2;

            double result = target.ConvertFromBaseUnit(sum);

            return new Quantity<U>(result, targetUnit);
        }

        public override bool Equals(object obj)
        {
            if (obj is not Quantity<U> other)
                return false;

            double base1 = ConvertToBase();

            double base2;

            if (other.unit is LengthUnit lu)
                base2 = lu.ConvertToBaseUnit(other.value);
            else if (other.unit is WeightUnit wu)
                base2 = wu.ConvertToBaseUnit(other.value);
            else
                throw new ArgumentException("Unsupported unit type");

            return Math.Round(base1, 2) == Math.Round(base2, 2);
        }

        public override int GetHashCode()
        {
            return ConvertToBase().GetHashCode();
        }
    }
}