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
            double baseValue = unit switch
            {
                LengthUnit.FEET => value * 12,
                LengthUnit.INCHES => value,
                LengthUnit.YARDS => value * 36,
                LengthUnit.CENTIMETERS => value * 0.393701,
                _ => throw new ArgumentException("Invalid unit")
            };

            return Math.Round(baseValue, 2);
        }

        public bool Compare(Length other)
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

            double convertedValue = targetUnit switch
            {
                LengthUnit.FEET => baseValue / 12,
                LengthUnit.INCHES => baseValue,
                LengthUnit.YARDS => baseValue / 36,
                LengthUnit.CENTIMETERS => baseValue / 0.393701,
                _ => throw new ArgumentException("Invalid unit")
            };

            convertedValue = Math.Round(convertedValue, 2);

            return new Length(convertedValue, targetUnit);
        }

        private double ConvertFromBaseToTargetUnit(double lengthInInches, LengthUnit targetUnit)
        {
            double result = targetUnit switch
            {
                LengthUnit.FEET => lengthInInches / 12,
                LengthUnit.INCHES => lengthInInches,
                LengthUnit.YARDS => lengthInInches / 36,
                LengthUnit.CENTIMETERS => lengthInInches / 0.393701,
                _ => throw new ArgumentException("Invalid unit")
            };

            return Math.Round(result, 2);
        }

        public Length Add(Length thatLength)
        {
            if (thatLength == null)
                throw new ArgumentException("Length to add cannot be null");

            double base1 = this.ConvertToBaseUnit();
            double base2 = thatLength.ConvertToBaseUnit();

            double sumBase = base1 + base2;

            double resultValue = ConvertFromBaseToTargetUnit(sumBase, this.unit);

            return new Length(resultValue, this.unit);
        }
    }
}