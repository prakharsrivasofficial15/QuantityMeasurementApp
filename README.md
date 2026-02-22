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