﻿using NUnit.Framework;
using ModelLayer.Entities;
using ModelLayer.Enums;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityMeasurementTests
    {
        // =======================
        // Length Tests with Simplified Quantity<T>
        // =======================

        [Test]
        public void GivenFeetAndFeet_WhenEqual_ShouldReturnTrue()
        {
            var length1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
            var length2 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);

            Assert.That(length1.Equals(length2), Is.True);
        }

        [Test]
        public void GivenInchesAndInches_WhenEqual_ShouldReturnTrue()
        {
            var length1 = new Quantity<LengthUnit>(1.0, LengthUnit.INCHES);
            var length2 = new Quantity<LengthUnit>(1.0, LengthUnit.INCHES);

            Assert.That(length1.Equals(length2), Is.True);
        }

        [Test]
        public void GivenFeetAndInches_WhenEquivalent_ShouldReturnTrue()
        {
            var length1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
            var length2 = new Quantity<LengthUnit>(12.0, LengthUnit.INCHES);

            Assert.That(length1.Equals(length2), Is.True);
        }

        [Test]
        public void GivenFeet_WhenDifferent_ShouldReturnFalse()
        {
            var length1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
            var length2 = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);

            Assert.That(length1.Equals(length2), Is.False);
        }

        [Test]
        public void GivenInches_WhenDifferent_ShouldReturnFalse()
        {
            var length1 = new Quantity<LengthUnit>(1.0, LengthUnit.INCHES);
            var length2 = new Quantity<LengthUnit>(2.0, LengthUnit.INCHES);

            Assert.That(length1.Equals(length2), Is.False);
        }

        [Test]
        public void GivenSameReference_ShouldReturnTrue()
        {
            var length = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);

            Assert.That(length.Equals(length), Is.True);
        }

        [Test]
        public void GivenNull_ShouldReturnFalse()
        {
            var length = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);

            Assert.That(length.Equals(null), Is.False);
        }

        [Test]
        public void GivenOneYard_WhenComparedWithThreeFeet_ShouldReturnTrue()
        {
            var yard = new Quantity<LengthUnit>(1.0, LengthUnit.YARDS);
            var feet = new Quantity<LengthUnit>(3.0, LengthUnit.FEET);

            Assert.That(yard.Equals(feet), Is.True);
        }

        [Test]
        public void GivenOneYard_WhenComparedWith36Inches_ShouldReturnTrue()
        {
            var yard = new Quantity<LengthUnit>(1.0, LengthUnit.YARDS);
            var inches = new Quantity<LengthUnit>(36.0, LengthUnit.INCHES);

            Assert.That(yard.Equals(inches), Is.True);
        }

        [Test]
        public void GivenCentimeter_WhenComparedWithInches_ShouldReturnTrue()
        {
            var cm = new Quantity<LengthUnit>(1.0, LengthUnit.CENTIMETERS);
            var inches = new Quantity<LengthUnit>(0.393701, LengthUnit.INCHES);

            Assert.That(cm.Equals(inches), Is.True);
        }

        [Test]
        public void GivenThreeFeet_WhenComparedWithOneYard_ShouldReturnTrue()
        {
            var feet = new Quantity<LengthUnit>(3.0, LengthUnit.FEET);
            var yard = new Quantity<LengthUnit>(1.0, LengthUnit.YARDS);

            Assert.That(feet.Equals(yard), Is.True);
        }

        [Test]
        public void GivenDifferentUnits_WhenNotEquivalent_ShouldReturnFalse()
        {
            var cm = new Quantity<LengthUnit>(1.0, LengthUnit.CENTIMETERS);
            var feet = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);

            Assert.That(cm.Equals(feet), Is.False);
        }

        [Test]
        public void ConvertFeetToInches()
        {
            var lengthInFeet = new Quantity<LengthUnit>(3.0, LengthUnit.FEET);
            var lengthInInches = lengthInFeet.ConvertTo(LengthUnit.INCHES);
            var expected = new Quantity<LengthUnit>(36.0, LengthUnit.INCHES);

            Assert.That(lengthInInches.Equals(expected), Is.True);
        }

        [Test]
        public void AddFeetAndInches()
        {
            var length1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
            var length2 = new Quantity<LengthUnit>(12.0, LengthUnit.INCHES);

            var result = length1.Add(length2);
            var expected = new Quantity<LengthUnit>(2.0, LengthUnit.FEET);

            Assert.That(result.Equals(expected), Is.True);
        }

        [Test]
        public void AddFeetAndInchesWithTargetUnitInches()
        {
            var length1 = new Quantity<LengthUnit>(1.0, LengthUnit.FEET);
            var length2 = new Quantity<LengthUnit>(12.0, LengthUnit.INCHES);

            var result = length1.Add(length2, LengthUnit.INCHES);
            var expected = new Quantity<LengthUnit>(24.0, LengthUnit.INCHES);

            Assert.That(result.Equals(expected), Is.True);
        }

        [Test]
        public void SubtractFeetAndInches()
        {
            var length1 = new Quantity<LengthUnit>(10.0, LengthUnit.FEET);
            var length2 = new Quantity<LengthUnit>(6.0, LengthUnit.INCHES);

            var result = length1.Subtract(length2);
            var expected = new Quantity<LengthUnit>(9.5, LengthUnit.FEET);

            Assert.That(result.Value, Is.EqualTo(9.5).Within(0.0001));
            Assert.That(result.Unit, Is.EqualTo(LengthUnit.FEET));
        }

        // =======================
        // Weight Tests with Simplified Quantity<T>
        // =======================

        [Test]
        public void KilogramEqualsKilogram()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);
            var w2 = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);

            Assert.That(w1.Equals(w2), Is.True);
        }

        [Test]
        public void KilogramEquals1000Grams()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);
            var w2 = new Quantity<WeightUnit>(1000, WeightUnit.GRAM);

            Assert.That(w1.Equals(w2), Is.True);
        }

        [Test]
        public void PoundEquals453Point592Grams()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.POUND);
            var w2 = new Quantity<WeightUnit>(453.592, WeightUnit.GRAM);

            Assert.That(w1.Equals(w2), Is.True);
        }

        [Test]
        public void TonneEquals1000000Grams()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.TONNE);
            var w2 = new Quantity<WeightUnit>(1000000, WeightUnit.GRAM);

            Assert.That(w1.Equals(w2), Is.True);
        }

        [Test]
        public void KilogramNotEqualToPound()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);
            var w2 = new Quantity<WeightUnit>(1, WeightUnit.POUND);

            Assert.That(w1.Equals(w2), Is.False);
        }

        [Test]
        public void AdditionOfWeightsEqualsExpected()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);
            var w2 = new Quantity<WeightUnit>(1000, WeightUnit.GRAM);

            var result = w1.Add(w2);
            var expected = new Quantity<WeightUnit>(2, WeightUnit.KILOGRAM);

            Assert.That(result.Equals(expected), Is.True);
        }

        [Test]
        public void AdditionWithTargetUnit()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);
            var w2 = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);

            var result = w1.Add(w2, WeightUnit.GRAM);

            Assert.That(result.Value, Is.EqualTo(2000));
            Assert.That(result.Unit, Is.EqualTo(WeightUnit.GRAM));
        }

        [Test]
        public void ConvertKilogramToGram()
        {
            var w = new Quantity<WeightUnit>(1, WeightUnit.KILOGRAM);
            var result = w.ConvertTo(WeightUnit.GRAM);

            Assert.That(result.Value, Is.EqualTo(1000));
        }

        [Test]
        public void ConvertPoundToKilogram()
        {
            var w = new Quantity<WeightUnit>(2.20462, WeightUnit.POUND);
            var result = w.ConvertTo(WeightUnit.KILOGRAM);

            Assert.That(result.Value, Is.EqualTo(1).Within(0.0001));
        }

        // =======================
        // Volume Tests with Simplified Quantity<T>
        // =======================

        [Test]
        public void LitreEqualsLitre()
        {
            var v1 = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
            var v2 = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);

            Assert.That(v1.Equals(v2), Is.True);
        }

        [Test]
        public void LitreEqualsMillilitre()
        {
            var v1 = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
            var v2 = new Quantity<VolumeUnit>(1000, VolumeUnit.MILLILITRE);

            Assert.That(v1.Equals(v2), Is.True);
        }

        [Test]
        public void GallonEquals3Point78541Litres()
        {
            var v1 = new Quantity<VolumeUnit>(1, VolumeUnit.GALLON);
            var v2 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.LITRE);

            Assert.That(v1.Equals(v2), Is.True);
        }

        [Test]
        public void ConvertLitreToMillilitre()
        {
            var volume = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
            var result = volume.ConvertTo(VolumeUnit.MILLILITRE);

            Assert.That(result.Value, Is.EqualTo(1000));
        }

        [Test]
        public void ConvertGallonToLitre()
        {
            var volume = new Quantity<VolumeUnit>(1, VolumeUnit.GALLON);
            var result = volume.ConvertTo(VolumeUnit.LITRE);

            Assert.That(result.Value, Is.EqualTo(3.78541).Within(0.00001));
        }

        [Test]
        public void AddLitreAndMillilitre()
        {
            var v1 = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
            var v2 = new Quantity<VolumeUnit>(1000, VolumeUnit.MILLILITRE);

            var result = v1.Add(v2);

            Assert.That(result.Value, Is.EqualTo(2));
        }

        [Test]
        public void AddWithTargetUnitMillilitre()
        {
            var v1 = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);
            var v2 = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);

            var result = v1.Add(v2, VolumeUnit.MILLILITRE);

            Assert.That(result.Value, Is.EqualTo(2000));
            Assert.That(result.Unit, Is.EqualTo(VolumeUnit.MILLILITRE));
        }

        [Test]
        public void SubtractLitres()
        {
            var v1 = new Quantity<VolumeUnit>(5, VolumeUnit.LITRE);
            var v2 = new Quantity<VolumeUnit>(2, VolumeUnit.LITRE);

            var result = v1.Subtract(v2);

            Assert.That(result.Value, Is.EqualTo(3));
        }

        [Test]
        public void DivideFeet()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.FEET);
            var q2 = new Quantity<LengthUnit>(2, LengthUnit.FEET);

            var result = q1.Divide(q2);

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void DivideDifferentUnits()
        {
            var q1 = new Quantity<LengthUnit>(24, LengthUnit.INCHES);
            var q2 = new Quantity<LengthUnit>(2, LengthUnit.FEET);

            var result = q1.Divide(q2);

            Assert.That(result, Is.EqualTo(1));
        }

        // =======================
        // Temperature Tests with Simplified Quantity<T>
        // =======================

        [Test]
        public void CelsiusEqualsCelsius()
        {
            var t1 = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);
            var t2 = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);

            Assert.That(t1.Equals(t2), Is.True);
        }

        [Test]
        public void FahrenheitEqualsFahrenheit()
        {
            var t1 = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);
            var t2 = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);

            Assert.That(t1.Equals(t2), Is.True);
        }

        [Test]
        public void ZeroCelsiusEqualsThirtyTwoFahrenheit()
        {
            var celsius = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);
            var fahrenheit = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);

            Assert.That(celsius.Equals(fahrenheit), Is.True);
        }

        [Test]
        public void HundredCelsiusEqualsTwoHundredTwelveFahrenheit()
        {
            var celsius = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var fahrenheit = new Quantity<TemperatureUnit>(212, TemperatureUnit.FAHRENHEIT);

            Assert.That(celsius.Equals(fahrenheit), Is.True);
        }

        [Test]
        public void NegativeFortyCelsiusEqualsNegativeFortyFahrenheit()
        {
            var celsius = new Quantity<TemperatureUnit>(-40, TemperatureUnit.CELSIUS);
            var fahrenheit = new Quantity<TemperatureUnit>(-40, TemperatureUnit.FAHRENHEIT);

            Assert.That(celsius.Equals(fahrenheit), Is.True);
        }

        [Test]
        public void ConvertCelsiusToFahrenheit()
        {
            var celsius = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var result = celsius.ConvertTo(TemperatureUnit.FAHRENHEIT);

            Assert.That(result.Value, Is.EqualTo(212));
        }

        [Test]
        public void ConvertFahrenheitToCelsius()
        {
            var fahrenheit = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);
            var result = fahrenheit.ConvertTo(TemperatureUnit.CELSIUS);

            Assert.That(result.Value, Is.EqualTo(0));
        }

        [Test]
        public void ConvertNegativeTemperature()
        {
            var celsius = new Quantity<TemperatureUnit>(-40, TemperatureUnit.CELSIUS);
            var result = celsius.ConvertTo(TemperatureUnit.FAHRENHEIT);

            Assert.That(result.Value, Is.EqualTo(-40));
        }

        [Test]
        public void TemperatureAdditionShouldThrowException()
        {
            var t1 = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var t2 = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);

            Assert.Throws<NotSupportedException>(() => t1.Add(t2));
        }

        [Test]
        public void TemperatureSubtractionShouldThrowException()
        {
            var t1 = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var t2 = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);

            Assert.Throws<NotSupportedException>(() => t1.Subtract(t2));
        }

        [Test]
        public void TemperatureDivisionShouldThrowException()
        {
            var t1 = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var t2 = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);

            Assert.Throws<NotSupportedException>(() => t1.Divide(t2));
        }

        // =======================
        // Cross Category Safety
        // =======================

        [Test]
        public void TemperatureNotEqualToLength()
        {
            var temp = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var length = new Quantity<LengthUnit>(100, LengthUnit.FEET);

            Assert.That(temp.Equals(length), Is.False);
        }

        [Test]
        public void TemperatureNotEqualToWeight()
        {
            var temp = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);
            var weight = new Quantity<WeightUnit>(50, WeightUnit.KILOGRAM);

            Assert.That(temp.Equals(weight), Is.False);
        }

        [Test]
        public void TemperatureNotEqualToVolume()
        {
            var temp = new Quantity<TemperatureUnit>(25, TemperatureUnit.CELSIUS);
            var volume = new Quantity<VolumeUnit>(25, VolumeUnit.LITRE);

            Assert.That(temp.Equals(volume), Is.False);
        }

        // =======================
        // Edge Case Tests
        // =======================

        [Test]
        public void SameReferenceTemperature()
        {
            var temp = new Quantity<TemperatureUnit>(25, TemperatureUnit.CELSIUS);

            Assert.That(temp.Equals(temp), Is.True);
        }

        [Test]
        public void NullComparisonReturnsFalse()
        {
            var temp = new Quantity<TemperatureUnit>(25, TemperatureUnit.CELSIUS);

            Assert.That(temp.Equals(null), Is.False);
        }

        [Test]
        public void DifferentTemperaturesNotEqual()
        {
            var t1 = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);
            var t2 = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);

            Assert.That(t1.Equals(t2), Is.False);
        }

        // =======================
        // Additional Tests for New Features
        // =======================

        [Test]
        public void ConvertCentimetersToInches()
        {
            var cm = new Quantity<LengthUnit>(10, LengthUnit.CENTIMETERS);
            var result = cm.ConvertTo(LengthUnit.INCHES);
            
            Assert.That(result.Value, Is.EqualTo(3.93701).Within(0.00001));
        }

        [Test]
        public void AddYardsAndFeet()
        {
            var yards = new Quantity<LengthUnit>(2, LengthUnit.YARDS);
            var feet = new Quantity<LengthUnit>(3, LengthUnit.FEET);
            
            var result = yards.Add(feet);
            var expected = new Quantity<LengthUnit>(9, LengthUnit.FEET);
            
            Assert.That(result.Equals(expected), Is.True);
        }

        [Test]
        public void SubtractGramsFromKilograms()
        {
            var kg = new Quantity<WeightUnit>(2, WeightUnit.KILOGRAM);
            var g = new Quantity<WeightUnit>(500, WeightUnit.GRAM);
            
            var result = kg.Subtract(g);
            var expected = new Quantity<WeightUnit>(1.5, WeightUnit.KILOGRAM);
            
            Assert.That(result.Value, Is.EqualTo(1.5).Within(0.0001));
        }

        [Test]
        public void GetValueReturnsCorrectValue()
        {
            var quantity = new Quantity<LengthUnit>(42.5, LengthUnit.FEET);
            Assert.That(quantity.Value, Is.EqualTo(42.5));
        }

        [Test]
        public void GetUnitReturnsCorrectUnit()
        {
            var quantity = new Quantity<LengthUnit>(10, LengthUnit.YARDS);
            Assert.That(quantity.Unit, Is.EqualTo(LengthUnit.YARDS));
        }

        [Test]
        public void ToStringFormatsCorrectly()
        {
            var quantity = new Quantity<LengthUnit>(12.5, LengthUnit.INCHES);
            Assert.That(quantity.ToString(), Is.EqualTo("12.5 INCHES"));
        }

        [Test]
        public void ToStringFormatsWholeNumbersWithoutDecimal()
        {
            var quantity = new Quantity<LengthUnit>(12, LengthUnit.INCHES);
            Assert.That(quantity.ToString(), Is.EqualTo("12 INCHES"));
        }
    }
}