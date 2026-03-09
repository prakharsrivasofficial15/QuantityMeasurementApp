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

        private enum ArithmeticOperation
        {
            Add,
            Subtract,
            Divide
        }

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
            return Add(other, unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);

            double result = PerformArithmetic(other, targetUnit, ArithmeticOperation.Add);

            return new Quantity<U>(Math.Round(result, 5), targetUnit);
        }

        public Quantity<U> Subtract(Quantity<U> other)
        {
            return Subtract(other, unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            ValidateArithmeticOperands(other, targetUnit, true);

            double result = PerformArithmetic(other, targetUnit, ArithmeticOperation.Subtract);

            return new Quantity<U>(Math.Round(result, 5), targetUnit);
        }

        public double Divide(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, default, false);

            return PerformArithmetic(other, default, ArithmeticOperation.Divide);
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

        private void ValidateArithmeticOperands(Quantity<U> other, U targetUnit, bool targetUnitRequired)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            if (!unit.GetType().Equals(other.unit.GetType()))
                throw new ArgumentException("Units belong to different measurement categories");

            if (double.IsNaN(value) || double.IsInfinity(value) ||
                double.IsNaN(other.value) || double.IsInfinity(other.value))
                throw new ArgumentException("Values must be finite numbers");

            if (targetUnitRequired && targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");
        }

        private double PerformArithmetic(Quantity<U> other, U targetUnit, ArithmeticOperation operation)
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

            double result = operation switch
            {
                ArithmeticOperation.Add => base1 + base2,
                ArithmeticOperation.Subtract => base1 - base2,
                ArithmeticOperation.Divide => base1 / base2,
                _ => throw new ArgumentException("Unsupported operation")
            };

            if (operation == ArithmeticOperation.Divide)
                return result;

            double converted;

            if (targetUnit is LengthUnit tlu)
                converted = tlu.ConvertFromBaseUnit(result);
            else if (targetUnit is WeightUnit twu)
                converted = twu.ConvertFromBaseUnit(result);
            else if (targetUnit is VolumeUnit tvu)
                converted = tvu.ConvertFromBaseUnit(result);
            else
                throw new ArgumentException("Unsupported unit type");

            return Math.Round(converted, 5);
        }

        public override int GetHashCode()
        {
            return ConvertToBase().GetHashCode();
        }
    }
}