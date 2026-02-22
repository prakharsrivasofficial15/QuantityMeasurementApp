using NUnit.Framework;
using QuantityMeasurementApp.UC2;

namespace QuantityMeasurementApp.Tests.UC2
{
    public class InchesEqualityTests
    {
        [Test]
        public void TestEquality_SameValue()
        {
            var i1 = new Inches(1.0);
            var i2 = new Inches(1.0);

            Assert.That(i1.Equals(i2), Is.True);
        }

        [Test]
        public void TestEquality_DifferentValue()
        {
            var i1 = new Inches(1.0);
            var i2 = new Inches(2.0);

            Assert.That(i1.Equals(i2), Is.False);
        }

        [Test]
        public void TestEquality_NullComparison()
        {
            var i1 = new Inches(1.0);

            Assert.That(i1.Equals(null), Is.False);
        }

        [Test]
        public void TestEquality_SameReference()
        {
            var i1 = new Inches(1.0);

            Assert.That(i1.Equals(i1), Is.True);
        }

        [Test]
        public void TestEquality_DifferentType()
        {
            var i1 = new Inches(1.0);
            object obj = new Feet(1.0);

            Assert.That(i1.Equals(obj), Is.False);
        }
    }
}