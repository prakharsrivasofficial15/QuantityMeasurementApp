using NUnit.Framework;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Enums;

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
    }
}