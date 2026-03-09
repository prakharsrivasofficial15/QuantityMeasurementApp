﻿using NUnit.Framework;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Enums;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityMeasurementTests
    {
        [Test]
        public void GivenFeetAndFeet_WhenEqual_ShouldReturnTrue()
        {
            Length length1 = new Length(1.0, LengthUnit.FEET);
            Length length2 = new Length(1.0, LengthUnit.FEET);

            Assert.That(length1.Equals(length2), Is.True);
        }

        [Test]
        public void GivenInchesAndInches_WhenEqual_ShouldReturnTrue()
        {
            Length length1 = new Length(1.0, LengthUnit.INCHES);
            Length length2 = new Length(1.0, LengthUnit.INCHES);

            Assert.That(length1.Equals(length2), Is.True);
        }

        [Test]
        public void GivenFeetAndInches_WhenEquivalent_ShouldReturnTrue()
        {
            Length length1 = new Length(1.0, LengthUnit.FEET);
            Length length2 = new Length(12.0, LengthUnit.INCHES);

            Assert.That(length1.Equals(length2), Is.True);
        }

        [Test]
        public void GivenFeet_WhenDifferent_ShouldReturnFalse()
        {
            Length length1 = new Length(1.0, LengthUnit.FEET);
            Length length2 = new Length(2.0, LengthUnit.FEET);

            Assert.That(length1.Equals(length2), Is.False);
        }

        [Test]
        public void GivenInches_WhenDifferent_ShouldReturnFalse()
        {
            Length length1 = new Length(1.0, LengthUnit.INCHES);
            Length length2 = new Length(2.0, LengthUnit.INCHES);

            Assert.That(length1.Equals(length2), Is.False);
        }

        [Test]
        public void GivenSameReference_ShouldReturnTrue()
        {
            Length length = new Length(1.0, LengthUnit.FEET);

            Assert.That(length.Equals(length), Is.True);
        }

        [Test]
        public void GivenNull_ShouldReturnFalse()
        {
            Length length = new Length(1.0, LengthUnit.FEET);

            Assert.That(length.Equals(null), Is.False);
        }

        [Test]
        public void GivenOneYard_WhenComparedWithThreeFeet_ShouldReturnTrue()
        {
            Length yard = new Length(1.0, LengthUnit.YARDS);
            Length feet = new Length(3.0, LengthUnit.FEET);

            Assert.That(yard.Equals(feet), Is.True);
        }

        [Test]
        public void GivenOneYard_WhenComparedWith36Inches_ShouldReturnTrue()
        {
            Length yard = new Length(1.0, LengthUnit.YARDS);
            Length inches = new Length(36.0, LengthUnit.INCHES);

            Assert.That(yard.Equals(inches), Is.True);
        }

        [Test]
        public void GivenCentimeter_WhenComparedWithInches_ShouldReturnTrue()
        {
            Length cm = new Length(1.0, LengthUnit.CENTIMETERS);
            Length inches = new Length(0.393701, LengthUnit.INCHES);

            Assert.That(cm.Equals(inches), Is.True);
        }

        [Test]
        public void GivenThreeFeet_WhenComparedWithOneYard_ShouldReturnTrue()
        {
            Length feet = new Length(3.0, LengthUnit.FEET);
            Length yard = new Length(1.0, LengthUnit.YARDS);

            Assert.That(feet.Equals(yard), Is.True);
        }

        [Test]
        public void GivenDifferentUnits_WhenNotEquivalent_ShouldReturnFalse()
        {
            Length cm = new Length(1.0, LengthUnit.CENTIMETERS);
            Length feet = new Length(1.0, LengthUnit.FEET);

            Assert.That(cm.Equals(feet), Is.False);
        }

        [Test]
        public void ConvertFeetToInches()
        {
            Length lengthInInches =
                Services.QuantityMeasurementApp.DemonstrateLengthConversion(
                    3.0,
                    LengthUnit.FEET,
                    LengthUnit.INCHES);

            Length expected = new Length(36.0, LengthUnit.INCHES);

            Assert.That(
                Services.QuantityMeasurementApp.DemonstrateLengthEquality(
                    lengthInInches,
                    expected),
                Is.True);
        }

        [Test]
        public void AddFeetAndInches()
        {
            Length length1 = new Length(1.0, LengthUnit.FEET);
            Length length2 = new Length(12.0, LengthUnit.INCHES);

            Length result = Services.QuantityMeasurementApp.DemonstrateLengthAddition(length1, length2);

            Length expected = new Length(2.0, LengthUnit.FEET);

            Assert.That(result.Equals(expected), Is.True);
        }

        [Test]
        public void AddFeetAndInchesWithTargetUnitInches()
        {
            Length length1 = new Length(1.0, LengthUnit.FEET);
            Length length2 = new Length(12.0, LengthUnit.INCHES);

            Length result =
                Services.QuantityMeasurementApp.DemonstrateLengthAddition(
                    length1,
                    length2,
                    LengthUnit.INCHES);

            Length expected = new Length(24.0, LengthUnit.INCHES);

            Assert.That(result.Equals(expected), Is.True);
        }

        // UC9 — Weight Equality Tests

        [Test]
        public void KilogramEqualsKilogram()
        {
            var w1 = new Weight(1, WeightUnit.KILOGRAM);
            var w2 = new Weight(1, WeightUnit.KILOGRAM);

            Assert.That(w1.Equals(w2), Is.True);
        }

        [Test]
        public void KilogramEquals1000Grams()
        {
            var w1 = new Weight(1, WeightUnit.KILOGRAM);
            var w2 = new Weight(1000, WeightUnit.GRAM);

            Assert.That(w1.Equals(w2), Is.True);
        }

        [Test]
        public void PoundEquals453Point592Grams()
        {
            var w1 = new Weight(1, WeightUnit.POUND);
            var w2 = new Weight(453.592, WeightUnit.GRAM);

            Assert.That(w1.Equals(w2), Is.True);
        }

        [Test]
        public void TonneEquals1000000Grams()
        {
            var w1 = new Weight(1, WeightUnit.TONNE);
            var w2 = new Weight(1000000, WeightUnit.GRAM);

            Assert.That(w1.Equals(w2), Is.True);
        }

        [Test]
        public void KilogramNotEqualToPound()
        {
            var w1 = new Weight(1, WeightUnit.KILOGRAM);
            var w2 = new Weight(1, WeightUnit.POUND);

            Assert.That(w1.Equals(w2), Is.False);
        }

        // UC9 — Addition Tests

        [Test]
        public void AdditionOfWeightsEqualsExpected()
        {
            var w1 = new Weight(1, WeightUnit.KILOGRAM);
            var w2 = new Weight(1000, WeightUnit.GRAM);

            var result = w1.Add(w2);

            Assert.That(result.Equals(new Weight(2, WeightUnit.KILOGRAM)), Is.True);
        }

        [Test]
        public void AdditionWithTargetUnit()
        {
            var w1 = new Weight(1, WeightUnit.KILOGRAM);
            var w2 = new Weight(1, WeightUnit.KILOGRAM);

            var result = w1.Add(w2, WeightUnit.GRAM);

            Assert.That(result.GetValue(), Is.EqualTo(2000));
            Assert.That(result.GetUnit(), Is.EqualTo(WeightUnit.GRAM));
        }

        // UC9 — Conversion Tests

        [Test]
        public void ConvertKilogramToGram()
        {
            var w = new Weight(1, WeightUnit.KILOGRAM);

            var result = w.ConvertTo(WeightUnit.GRAM);

            Assert.That(result.GetValue(), Is.EqualTo(1000));
        }

        [Test]
        public void ConvertPoundToKilogram()
        {
            var w = new Weight(2.20462, WeightUnit.POUND);

            var result = w.ConvertTo(WeightUnit.KILOGRAM);

            Assert.That(result.GetValue(), Is.EqualTo(1));
        }

        [Test]
        public void UC10_GivenFeetAndInches_WhenEquivalent_ShouldReturnTrue()
        {
            var feet = new Quantity<LengthUnit>(1, LengthUnit.FEET);
            var inches = new Quantity<LengthUnit>(12, LengthUnit.INCHES);

            Assert.That(feet.Equals(inches), Is.True);
        }
    }
}