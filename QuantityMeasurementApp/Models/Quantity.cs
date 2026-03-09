using QuantityMeasurementApp.Enums;
using QuantityMeasurementApp.Interfaces;
using QuantityMeasurementApp.Services;
using QuantityMeasurementApp.Extensions;

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

            if (unit is VolumeUnit vu)
                return vu.ConvertToBaseUnit(value);

            throw new ArgumentException("Unsupported unit type");
        }

        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue;

            if (unit is LengthUnit lu)
                baseValue = lu.ConvertToBaseUnit(value);
            else if (unit is WeightUnit wu)
                baseValue = wu.ConvertToBaseUnit(value);
            else if (unit is VolumeUnit vu)
                baseValue = vu.ConvertToBaseUnit(value);
            else
                throw new ArgumentException("Unsupported unit type");

            double converted;

            if (targetUnit is LengthUnit tlu)
                converted = tlu.ConvertFromBaseUnit(baseValue);
            else if (targetUnit is WeightUnit twu)
                converted = twu.ConvertFromBaseUnit(baseValue);
            else if (targetUnit is VolumeUnit tvu)
                converted = tvu.ConvertFromBaseUnit(baseValue);
            else
                throw new ArgumentException("Unsupported unit type");

            return new Quantity<U>(Math.Round(converted, 5), targetUnit);
        }

        public Quantity<U> Add(Quantity<U> other)
        {
            double base1 = ConvertToBase();
            double base2;

            if (other.unit is LengthUnit lu)
                base2 = lu.ConvertToBaseUnit(other.value);
            else if (other.unit is WeightUnit wu)
                base2 = wu.ConvertToBaseUnit(other.value);
            else if (other.unit is VolumeUnit vu)
                base2 = vu.ConvertToBaseUnit(other.value);
            else
                throw new ArgumentException("Unsupported unit type");

            double sum = base1 + base2;

            double result;

            if (unit is LengthUnit tlu)
                result = tlu.ConvertFromBaseUnit(sum);
            else if (unit is WeightUnit twu)
                result = twu.ConvertFromBaseUnit(sum);
            else if (unit is VolumeUnit tvu)
                result = tvu.ConvertFromBaseUnit(sum);
            else
                throw new ArgumentException("Unsupported unit type");

            return new Quantity<U>(result, unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            double base1 = ConvertToBase();
            double base2;

            if (other.unit is LengthUnit lu)
                base2 = lu.ConvertToBaseUnit(other.value);
            else if (other.unit is WeightUnit wu)
                base2 = wu.ConvertToBaseUnit(other.value);
            else if (other.unit is VolumeUnit vu)
                base2 = vu.ConvertToBaseUnit(other.value);
            else
                throw new ArgumentException("Unsupported unit type");

            double sum = base1 + base2;

            double result;

            if (targetUnit is LengthUnit tlu)
                result = tlu.ConvertFromBaseUnit(sum);
            else if (targetUnit is WeightUnit twu)
                result = twu.ConvertFromBaseUnit(sum);
            else if (targetUnit is VolumeUnit tvu)
                result = tvu.ConvertFromBaseUnit(sum);
            else
                throw new ArgumentException("Unsupported unit type");

            return new Quantity<U>(result, targetUnit);
        }

        public Quantity<U> Subtract(Quantity<U> other)
        {
            double base1 = ConvertToBase();
            double base2;

            if (other.unit is LengthUnit lu)
                base2 = lu.ConvertToBaseUnit(other.value);
            else if (other.unit is WeightUnit wu)
                base2 = wu.ConvertToBaseUnit(other.value);
            else if (other.unit is VolumeUnit vu)
                base2 = vu.ConvertToBaseUnit(other.value);
            else
                throw new ArgumentException("Unsupported unit type");

            double diff = base1 - base2;

            double result;

            if (unit is LengthUnit tlu)
                result = tlu.ConvertFromBaseUnit(diff);
            else if (unit is WeightUnit twu)
                result = twu.ConvertFromBaseUnit(diff);
            else if (unit is VolumeUnit tvu)
                result = tvu.ConvertFromBaseUnit(diff);
            else
                throw new ArgumentException("Unsupported unit type");

            return new Quantity<U>(Math.Round(result, 5), unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            if (other == null)
                throw new ArgumentException("Quantity cannot be null");

            if (targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");

            dynamic thisUnit = this.unit;
            dynamic otherUnit = other.unit;
            dynamic target = targetUnit;

            double base1 = thisUnit.ConvertToBaseUnit(this.value);
            double base2 = otherUnit.ConvertToBaseUnit(other.value);

            double resultBase = base1 - base2;

            double converted = target.ConvertFromBaseUnit(resultBase);

            converted = Math.Round(converted, 2);

            return new Quantity<U>(converted, targetUnit);
        }

        public double Divide(Quantity<U> other)
        {
            double base1 = ConvertToBase();
            double base2;

            if (other.unit is LengthUnit lu)
                base2 = lu.ConvertToBaseUnit(other.value);
            else if (other.unit is WeightUnit wu)
                base2 = wu.ConvertToBaseUnit(other.value);
            else if (other.unit is VolumeUnit vu)
                base2 = vu.ConvertToBaseUnit(other.value);
            else
                throw new ArgumentException("Unsupported unit type");

            return base1 / base2;
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
            else if (other.unit is VolumeUnit vu)
                base2 = vu.ConvertToBaseUnit(other.value);
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