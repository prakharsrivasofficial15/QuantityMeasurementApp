using NUnit.Framework;
using QuantityMeasurementApp.UC3;

namespace QuantityMeasurementApp.Tests.UC4
{
    public class LengthExtendedTests
    {
        [Test]
        public void Yard_To_Yard_SameValue_ShouldBeEqual()
        {
            var l1 = new Length(1.0, LengthUnit.Yards);
            var l2 = new Length(1.0, LengthUnit.Yards);

            Assert.That(l1.Equals(l2), Is.True);
        }

        [Test]
        public void Yard_To_Feet_ShouldBeEqual()
        {
            var yard = new Length(1.0, LengthUnit.Yards);
            var feet = new Length(3.0, LengthUnit.Feet);

            Assert.That(yard.Equals(feet), Is.True);
        }

        [Test]
        public void Yard_To_Inches_ShouldBeEqual()
        {
            var yard = new Length(1.0, LengthUnit.Yards);
            var inches = new Length(36.0, LengthUnit.Inches);

            Assert.That(yard.Equals(inches), Is.True);
        }

        [Test]
        public void Centimeter_To_Inches_ShouldBeEqual()
        {
            var cm = new Length(1.0, LengthUnit.Centimeters);
            var inches = new Length(0.393701, LengthUnit.Inches);

            Assert.That(cm.Equals(inches), Is.True);
        }

        [Test]
        public void Complex_Transitive_Property_ShouldHold()
        {
            var yard = new Length(1.0, LengthUnit.Yards);
            var feet = new Length(3.0, LengthUnit.Feet);
            var inches = new Length(36.0, LengthUnit.Inches);

            Assert.That(yard.Equals(feet), Is.True);
            Assert.That(feet.Equals(inches), Is.True);
            Assert.That(yard.Equals(inches), Is.True);
        }

        [Test]
        public void Yard_DifferentValue_ShouldNotBeEqual()
        {
            var l1 = new Length(1.0, LengthUnit.Yards);
            var l2 = new Length(2.0, LengthUnit.Yards);

            Assert.That(l1.Equals(l2), Is.False);
        }

        [Test]
        public void NullComparison_ShouldReturnFalse()
        {
            var length = new Length(1.0, LengthUnit.Yards);

            Assert.That(length.Equals(null), Is.False);
        }
    }
}