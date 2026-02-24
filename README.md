## QuantityMeasurementApp
## Executive Summary

QuantityMeasurementApp is a structured, incremental application designed to model, compare, convert, and operate on measurable quantities such as length and weight.

# QuantityMeasurementApp

A small utility and test suite for comparing and validating physical-length quantities (feet/inches). It provides equality checks and a foundation for future unit conversions and comparisons.

## Features
- Feet equality check — compare feet values with clear equality rules.
- Inches equality check — compare inches values and normalize across units.
- Extensible NUnit test suite covering expected behaviors.

## Prerequisites
- .NET SDK 6.0 or later

## Getting Started
Build the project and run tests:

```bash
dotnet build
dotnet test
```

## Tech Stack
- C#
- .NET
- NUnit

## Branch Strategy
- `main` → Production-ready
- `develop` → Integration branch
- `feature/*` → Individual features

## Contributing
Contributions welcome — open an issue or submit a pull request with a clear description of changes.

## License
Add a LICENSE file (for example, MIT) to clarify terms for reuse.

## Contact
Open an issue in this repository for questions or suggestions.

## Use Case: UC1 – Feet Measurement Equality

### Overview
This use case implements equality comparison for measurements expressed in Feet.

The system compares two numerical values in feet and determines whether they are equal while handling null checks, type safety, and floating-point precision.

### Features Implemented
- Value-based equality comparison
- Proper override of `Equals()` and `GetHashCode()`
- Null safety handling
- Type safety enforcement
- NUnit test coverage

### Scope
- Supports Feet-to-Feet comparison only
- Does NOT support Inches or cross-unit comparison
- No unit conversion

### Concepts Covered
- Equality Contract (Reflexive, Symmetric, Transitive)
- Floating-point comparison using `Double.Compare`
- Encapsulation
- NUnit testing best practices

### How to Run
```bash
dotnet test
```

### Notes
Include this use case in the project's documentation to make the current scope and guarantees explicit to contributors and reviewers.

---

# UC2 – Feet and Inches Measurement Equality

## Overview
This use case extends UC1 by introducing Inches measurement equality.

Feet and Inches are treated as separate classes. Cross-unit comparison is NOT supported in this use case.

## Features Implemented
- Feet equality comparison
- Inches equality comparison
- Null safety and type checking
- NUnit test coverage for both units

## Scope
- Feet-to-Feet comparison
- Inches-to-Inches comparison
- No cross-unit conversion
- No abstraction (intentional duplication to demonstrate DRY violation)

## Concepts Covered
- Equality Contract
- Floating-point comparison
- Type safety
- DRY violation awareness
- Unit-level isolation

## How to Run
```bash
dotnet test
```

Include this use case in the README to document the current limitations and testing coverage.

---

# UC3 – Generic Length Class (DRY Refactor)

## Overview
This use case refactors UC1 and UC2 to eliminate duplication by introducing a single generic `Length` class.

A `LengthUnit` enum is used to define supported units, and cross-unit comparison is supported through conversion to a common base unit.

## Features Implemented
- Single `Length` class for all length measurements
- `LengthUnit` enum (Feet, Inches)
- Cross-unit equality (1 ft == 12 inches)
- Conversion to base unit (inches)
- `IEquatable` implementation
- Full NUnit test coverage

## Scope
- Supports Feet and Inches
- Supports cross-unit equality
- No arithmetic operations
- No additional unit types beyond Feet and Inches

## Concepts Covered
- DRY Principle
- Enum usage in C#
- Abstraction
- Encapsulation
- Cross-unit conversion logic
- Equality contract
- SOLID principles

## Example
Quantity(1.0, Feet) == Quantity(12.0, Inches) → true

## How to Run
```bash
dotnet test
```

Documenting UC3 clarifies the refactor and the supported behaviors for contributors and reviewers.

---

# UC4 – Extended Length Support & Equality

## Overview
UC4 extends the `Length` value object to support additional units and cross-unit equality using a common base unit (feet) for normalization. Supported units in this use case:

- FEET
- INCHES
- YARDS
- CENTIMETERS

## Design Overview

1. Length Value Object

	 - Represents an immutable measurement with two properties:
		 - `double Value`
		 - `LengthUnit Unit`
	 - Overrides `Equals()` and `GetHashCode()` to implement value-object semantics.
	 - Normalizes values to a common base unit (FEET) for equality checks.

2. LengthUnit Enum (conceptual)

```csharp
public enum LengthUnit
{
		FEET,       // base unit
		INCHES,     // 1 foot == 12 inches
		YARDS,      // 1 yard == 3 feet
		CENTIMETERS // 1 cm == 0.0328084 feet
}
```

Conversion factors are best stored in a separate, type-safe mapping (for example an immutable dictionary or extension method) so floating-point factors may be used alongside the enum values.

### Example conversion map (conceptual)

```csharp
static readonly IReadOnlyDictionary<LengthUnit, double> FeetPerUnit = new Dictionary<LengthUnit,double>
{
		{ LengthUnit.FEET, 1.0 },
		{ LengthUnit.INCHES, 1.0 / 12.0 },
		{ LengthUnit.YARDS, 3.0 },
		{ LengthUnit.CENTIMETERS, 0.0328084 }
};
```

## Equality Logic

- Normalize both values to feet using the conversion factor: `valueInFeet = Value * FeetPerUnit[Unit]`.
- Two `Length` instances are considered equal when:

	```text
	|value1InFeet - value2InFeet| < epsilon
	```

	Use a small epsilon (for example `1e-6`) to handle floating-point imprecision.

## Features Implemented

- Same-unit equality
- Cross-unit equality (e.g., feet ↔ inches, feet ↔ yards, feet ↔ centimeters)
- Normalization to base unit (feet)
- Immutability and value-object semantics
- Null safety and type checks
- Floating-point tolerance handling

## Sample NUnit Tests (examples)

- `Assert.AreEqual(new Length(1.0, LengthUnit.FEET), new Length(12.0, LengthUnit.INCHES));`
- `Assert.AreEqual(new Length(3.0, LengthUnit.FEET), new Length(1.0, LengthUnit.YARDS));`
- `Assert.AreEqual(new Length(30.48, LengthUnit.CENTIMETERS), new Length(1.0, LengthUnit.FEET));`
- Different magnitudes are not equal
- Comparing with `null` returns `false`

## Concepts Demonstrated

- Value Object design and immutability
- Enum for unit identification with conversion factors stored separately
- Base-unit normalization for cross-unit equality
- Overriding `Equals()` and `GetHashCode()`
- Floating-point tolerance in equality checks
- NUnit test coverage for domain guarantees

## How to Run
```bash
dotnet test
```

Notes: This UC4 section documents the extended-unit behavior and testing expectations. Implementation details (class names, method signatures, exact epsilon) can be adjusted to match the existing code style.

---

# UC5 – Unit-to-Unit Conversion API

## Overview
UC5 extends UC4 by adding an explicit conversion API that returns numeric values converted from a source unit into a target unit. This is a stateless, reusable utility suitable for domain and service layers.

## API Design

Static conversion method (conceptual):

```csharp
public static double ConvertUnits(
	double value,
	LengthUnit source,
	LengthUnit target)
```

## Conversion Formula

All conversions use the FEET base-unit normalization:

- `baseValue = value × sourceFactor`
- `result = baseValue ÷ targetFactor`

Where `sourceFactor` and `targetFactor` are the number of feet per unit for the respective enum values.

## Example Conversions

- `1 FEET → INCHES` → `12`
- `3 YARDS → FEET` → `9`
- `36 INCHES → YARDS` → `1`
- `2.54 CENTIMETERS → INCHES` → ≈ `1`
- `0 FEET → INCHES` → `0`

## Features Implemented

- Same-unit conversion
- Cross-unit conversion
- Bidirectional accuracy
- Zero and negative value handling
- Large/small magnitude safety
- NaN and Infinity validation
- Null/invalid unit validation (throw meaningful exceptions)
- Optional precision rounding parameter

## NUnit Test Coverage (examples)

- Feet → Inches
- Inches → Feet
- Yards → Inches
- Centimeters → Inches
- Round-trip conversion (A → B → A)
- Precision tolerance tests
- Invalid unit / NaN / Infinity exception tests

## Concepts Demonstrated

- Enum-driven conversion architecture
- Base-unit normalization for a single source of truth
- Reusable, stateless conversion logic
- Input validation and exception handling
- Floating-point precision control and rounding
- Clean, minimal API surface separating domain from service responsibilities

## How to Run
```bash
dotnet test
```

Notes: Implementations should validate `value` for NaN/Infinity and throw `ArgumentException` or similar for invalid `LengthUnit` values. Consider exposing an overload that accepts a `precision` or `epsilon` for callers that need controlled rounding.