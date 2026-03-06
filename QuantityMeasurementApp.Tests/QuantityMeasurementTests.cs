using NUnit.Framework;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Enums;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityMeasurementTests
    {
        [Test]
        public void GivenSameFeetValue_WhenCompared_ShouldReturnTrue()
        {
            Quantity value1 = new Quantity(1.0, UnitType.Feet);
            Quantity value2 = new Quantity(1.0, UnitType.Feet);

            bool result = value1.Equals(value2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenDifferentFeetValue_WhenCompared_ShouldReturnFalse()
        {
            Quantity value1 = new Quantity(1.0, UnitType.Feet);
            Quantity value2 = new Quantity(2.0, UnitType.Feet);

            bool result = value1.Equals(value2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenSameInchValue_WhenCompared_ShouldReturnTrue()
        {
            Quantity value1 = new Quantity(1.0, UnitType.Inch);
            Quantity value2 = new Quantity(1.0, UnitType.Inch);

            bool result = value1.Equals(value2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenDifferentInchValue_WhenCompared_ShouldReturnFalse()
        {
            Quantity value1 = new Quantity(1.0, UnitType.Inch);
            Quantity value2 = new Quantity(2.0, UnitType.Inch);

            bool result = value1.Equals(value2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenFeetAndInch_WhenCompared_ShouldReturnFalse()
        {
            Quantity feet = new Quantity(1.0, UnitType.Feet);
            Quantity inch = new Quantity(1.0, UnitType.Inch);

            bool result = feet.Equals(inch);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenQuantity_WhenComparedWithNull_ShouldReturnFalse()
        {
            Quantity value = new Quantity(1.0, UnitType.Feet);

            bool result = value.Equals(null);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenQuantity_WhenComparedWithDifferentType_ShouldReturnFalse()
        {
            Quantity value = new Quantity(1.0, UnitType.Feet);

            bool result = value.Equals("1.0");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenQuantity_WhenComparedWithSameReference_ShouldReturnTrue()
        {
            Quantity value = new Quantity(1.0, UnitType.Feet);

            bool result = value.Equals(value);

            Assert.That(result, Is.True);
        }
    }
}