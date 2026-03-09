using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Enums;
using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp.Services
{
    public static class QuantityMeasurementApp
    {
        public static bool DemonstrateLengthEquality(Length l1, Length l2)
        {
            return l1.Equals(l2);
        }

        public static Length DemonstrateLengthConversion(
            double value,
            LengthUnit fromUnit,
            LengthUnit toUnit)
        {
            var length = new Length(value, fromUnit);
            return length.ConvertTo(toUnit);
        }

        public static Length DemonstrateLengthConversion(
            Length length,
            LengthUnit toUnit)
        {
            return length.ConvertTo(toUnit);
        }

        public static Length DemonstrateLengthAddition(Length length1, Length length2)
        {
            return length1.Add(length2);
        }

        public static Length DemonstrateLengthAddition(
            Length length1,
            Length length2,
            LengthUnit targetUnit)
        {
            return length1.Add(length2, targetUnit);
        }

        //UC-9
        public static bool DemonstrateWeightEquality(Weight w1, Weight w2)
        {
            return w1.Equals(w2);
        }

        public static bool DemonstrateWeightComparison(
            double value1,
            WeightUnit unit1,
            double value2,
            WeightUnit unit2)
        {
            Weight w1 = new Weight(value1, unit1);
            Weight w2 = new Weight(value2, unit2);

            return w1.Equals(w2);
        }

        public static Weight DemonstrateWeightConversion(
            double value,
            WeightUnit fromUnit,
            WeightUnit toUnit)
        {
            Weight weight = new Weight(value, fromUnit);
            return weight.ConvertTo(toUnit);
        }

        public static Weight DemonstrateWeightConversion(
            Weight weight,
            WeightUnit toUnit)
        {
            return weight.ConvertTo(toUnit);
        }

        public static Weight DemonstrateWeightAddition(
            Weight w1,
            Weight w2)
        {
            return w1.Add(w2);
        }

        public static Weight DemonstrateWeightAddition(
            Weight w1,
            Weight w2,
            WeightUnit targetUnit)
        {
            return w1.Add(w2, targetUnit);
        }

        public static bool DemonstrateEquality<U>(Quantity<U> q1, Quantity<U> q2)
            where U : IMeasurable
        {
            return q1.Equals(q2);
        }

        public static Quantity<U> DemonstrateConversion<U>(Quantity<U> quantity, U targetUnit)
            where U : IMeasurable
        {
            return quantity.ConvertTo(targetUnit);
        }

        public static Quantity<U> DemonstrateAddition<U>(Quantity<U> q1, Quantity<U> q2)
            where U : IMeasurable
        {
            return q1.Add(q2);
        }

        public static Quantity<U> DemonstrateAddition<U>(Quantity<U> q1, Quantity<U> q2, U targetUnit)
            where U : IMeasurable
        {
            return q1.Add(q2, targetUnit);
        }
    }
}