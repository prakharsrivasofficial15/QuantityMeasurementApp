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
            Length length1 = new Length(1.0, LengthUnit.Feet);
            Length length2 = new Length(1.0, LengthUnit.Feet);

            bool result = length1.Equals(length2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenInchesAndInches_WhenEqual_ShouldReturnTrue()
        {
            Length length1 = new Length(1.0, LengthUnit.Inch);
            Length length2 = new Length(1.0, LengthUnit.Inch);

            bool result = length1.Equals(length2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenFeetAndInches_WhenEquivalent_ShouldReturnTrue()
        {
            Length length1 = new Length(1.0, LengthUnit.Feet);
            Length length2 = new Length(12.0, LengthUnit.Inch);

            bool result = length1.Equals(length2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenFeet_WhenDifferent_ShouldReturnFalse()
        {
            Length length1 = new Length(1.0, LengthUnit.Feet);
            Length length2 = new Length(2.0, LengthUnit.Feet);

            bool result = length1.Equals(length2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenInches_WhenDifferent_ShouldReturnFalse()
        {
            Length length1 = new Length(1.0, LengthUnit.Inch);
            Length length2 = new Length(2.0, LengthUnit.Inch);

            bool result = length1.Equals(length2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenSameReference_ShouldReturnTrue()
        {
            Length length = new Length(1.0, LengthUnit.Feet);

            bool result = length.Equals(length);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenNull_ShouldReturnFalse()
        {
            Length length = new Length(1.0, LengthUnit.Feet);

            bool result = length.Equals(null);

            Assert.That(result, Is.False);
        }
    }
}