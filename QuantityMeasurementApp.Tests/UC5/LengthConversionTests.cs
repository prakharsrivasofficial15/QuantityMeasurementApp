using System;
using NUnit.Framework;
using QuantityMeasurementApp.UC5;

namespace QuantityMeasurementApp.Tests.UC5
{
    [TestFixture]
    public class LengthConversionTests
    {
        [Test]
        public void Convert_FeetToInches_Returns12()
        {
            double result = QuantityMeasurementService
                .ConvertUnits(1.0, LengthUnit.Feet, LengthUnit.Inches);

            Assert.That(result, Is.EqualTo(12.0));
        }

        [Test]
        public void Convert_YardsToFeet_Returns3()
        {
            double result = QuantityMeasurementService
                .ConvertUnits(1.0, LengthUnit.Yards, LengthUnit.Feet);

            Assert.That(result, Is.EqualTo(3.0));
        }

        [Test]
        public void Convert_CmToInches_ReturnsApprox()
        {
            double result = QuantityMeasurementService
                .ConvertUnits(1.0, LengthUnit.Centimeters, LengthUnit.Inches);

            Assert.That(result, Is.EqualTo(0.393701).Within(0.000001));
        }

        [Test]
        public void Convert_InvalidValue_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                QuantityMeasurementService.ConvertUnits(
                    double.NaN,
                    LengthUnit.Feet,
                    LengthUnit.Inches));
        }
    }
}