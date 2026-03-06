using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityMeasurementTests
    {
        [Test]
        public void GivenSameFeetValue_WhenCompared_ShouldReturnTrue()
        {
            Quantity feet1 = new Quantity(1.0);
            Quantity feet2 = new Quantity(1.0);

            bool result = feet1.Equals(feet2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GivenDifferentFeetValue_WhenCompared_ShouldReturnFalse()
        {
            Quantity feet1 = new Quantity(1.0);
            Quantity feet2 = new Quantity(2.0);

            bool result = feet1.Equals(feet2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenFeetValue_WhenComparedWithNull_ShouldReturnFalse()
        {
            Quantity feet = new Quantity(1.0);

            bool result = feet.Equals(null);

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenFeetValue_WhenComparedWithDifferentType_ShouldReturnFalse()
        {
            Quantity feet = new Quantity(1.0);

            bool result = feet.Equals("1.0");

            Assert.That(result, Is.False);
        }

        [Test]
        public void GivenFeetValue_WhenComparedWithSameReference_ShouldReturnTrue()
        {
            Quantity feet = new Quantity(1.0);

            bool result = feet.Equals(feet);

            Assert.That(result, Is.True);
        }
    }
}