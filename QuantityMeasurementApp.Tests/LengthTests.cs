using NUnit.Framework;
using QuantityMeasurementApp.UC3;

namespace QuantityMeasurementApp.Tests.UC3
{
    public class LengthTests
    {
        [Test]
        public void Feet_To_Feet_SameValue_ShouldBeEqual()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(1.0, LengthUnit.Feet);

            Assert.That(l1.Equals(l2), Is.True);
        }

        [Test]
        public void Inches_To_Inches_SameValue_ShouldBeEqual()
        {
            var l1 = new Length(5.0, LengthUnit.Inches);
            var l2 = new Length(5.0, LengthUnit.Inches);

            Assert.That(l1.Equals(l2), Is.True);
        }

        [Test]
        public void Feet_To_Inches_Equivalent_ShouldBeEqual()
        {
            var feet = new Length(1.0, LengthUnit.Feet);
            var inches = new Length(12.0, LengthUnit.Inches);

            Assert.That(feet.Equals(inches), Is.True);
        }

        [Test]
        public void Inches_To_Feet_Equivalent_ShouldBeEqual()
        {
            var inches = new Length(12.0, LengthUnit.Inches);
            var feet = new Length(1.0, LengthUnit.Feet);

            Assert.That(inches.Equals(feet), Is.True);
        }

        [Test]
        public void Feet_DifferentValue_ShouldNotBeEqual()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(2.0, LengthUnit.Feet);

            Assert.That(l1.Equals(l2), Is.False);
        }

        [Test]
        public void Inches_DifferentValue_ShouldNotBeEqual()
        {
            var l1 = new Length(1.0, LengthUnit.Inches);
            var l2 = new Length(2.0, LengthUnit.Inches);

            Assert.That(l1.Equals(l2), Is.False);
        }

        [Test]
        public void SameReference_ShouldBeEqual()
        {
            var length = new Length(1.0, LengthUnit.Feet);

            Assert.That(length.Equals(length), Is.True);
        }

        [Test]
        public void NullComparison_ShouldReturnFalse()
        {
            var length = new Length(1.0, LengthUnit.Feet);

            Assert.That(length.Equals(null), Is.False);
        }
    }
}