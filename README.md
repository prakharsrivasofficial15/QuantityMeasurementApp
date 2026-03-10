"# 📏 Quantity Measurement Application

A robust measurement conversion and arithmetic system built using C#, .NET, OOP and SOLID principles.

This application supports operations across Length, Weight and Volume with a clean extensible architecture using generics and unit conversion strategies.

## 📋 Table of Contents

- [Overview](#overview)
- [Features by Use Case](#features-by-use-case)
- [Architecture](#architecture)
- [Conversion Tables](#conversion-tables)
- [Tech Stack](#tech-stack)
- [Installation](#installation)
- [Usage Guide](#usage-guide)
- [Testing](#testing)
- [Project Structure](#project-structure)
- [Branch Strategy](#branch-strategy)
- [Contributing](#contributing)
- [License](#license)

## 📌 Overview

The Quantity Measurement Application provides a flexible system for performing operations on measurable quantities such as:

- Length
- Weight
- Volume

The system supports:

- ✔ Unit comparison
- ✔ Unit conversion
- ✔ Addition
- ✔ Subtraction
- ✔ Division

The architecture ensures:

- Extensibility for new measurement types
- Reusable conversion logic
- Strong unit testing
- SOLID design compliance

## 🚀 Features by Use Case

| Use Case | Feature |
|----------|---------|
| UC1 | Compare same units |
| UC2 | Compare different units |
| UC3 | Handle reference equality and null |
| UC4 | Support new length units |
| UC5 | Convert between units |
| UC6 | Add quantities |
| UC7 | Add with target unit |
| UC9 | Weight measurement support |
| UC10 | Generic Quantity model |
| UC11 | Volume measurement support |
| UC12 | Subtraction operations |
| UC13 | Division operations |

## 🏗 Architecture

The project follows a layered object-oriented architecture.

### Architecture Principles

The system uses:

- Encapsulation
- Abstraction
- Generics
- Extension Methods
- SOLID Principles

### Layer Responsibilities

| Layer | Responsibility |
|-------|----------------|
| Enums | Unit definitions |
| Models | Core domain entities |
| Interfaces | Abstraction contracts |
| Extensions | Conversion logic |
| Services | Application services |
| Tests | Unit testing |

## 📐 Conversion Tables

### Length Conversion

| Unit | Base Unit (Inches) |
|------|-------------------|
| 1 Foot | 12 Inches |
| 1 Yard | 36 Inches |
| 1 Centimeter | 0.393701 Inches |

**Example conversions:**

| Conversion | Result |
|------------|--------|
| 1 Foot → Inches | 12 |
| 3 Feet → Inches | 36 |
| 1 Yard → Feet | 3 |

### Weight Conversion

| Unit | Base Unit (Gram) |
|------|------------------|
| 1 Kilogram | 1000 g |
| 1 Pound | 453.592 g |
| 1 Tonne | 1,000,000 g |

**Example conversions:**

| Conversion | Result |
|------------|--------|
| 1 Kilogram → Gram | 1000 |
| 2.20462 Pounds → Kilogram | 1 |
| 1 Tonne → Gram | 1000000 |

### Volume Conversion

| Unit | Base Unit (Millilitre) |
|------|------------------------|
| 1 Litre | 1000 ml |
| 1 Gallon | 3785.41 ml |

**Example conversions:**

| Conversion | Result |
|------------|--------|
| 1 Litre → Millilitre | 1000 |
| 1 Gallon → Litre | 3.78541 |

## 🛠 Tech Stack

| Technology | Purpose |
|------------|---------|
| C# | Programming language |
| .NET 10 | Application runtime |
| OOP | System design |
| SOLID | Maintainable architecture |
| NUnit | Unit testing |
| Coverlet | Code coverage |

## ⚙ Installation

### Clone the repository

```bash
git clone https://github.com/your-username/quantitymeasurementapp.git
```

### Navigate to project

```bash
cd quantitymeasurementapp
```

### Build the project

```bash
dotnet build
```

### Run the application

```bash
dotnet run
```

## 📖 Usage Guide

### Length Conversion

```csharp
Length length = new Length(3, LengthUnit.FEET);
Length result = length.ConvertTo(LengthUnit.INCHES);
```

**Result:**

```
36 inches
```

### Length Addition

```csharp
Length l1 = new Length(1, LengthUnit.FEET);
Length l2 = new Length(12, LengthUnit.INCHES);

Length result = l1.Add(l2);
```

**Result:**

```
2 feet
```

### Generic Quantity Usage

```csharp
var volume = new Quantity<VolumeUnit>(1, VolumeUnit.LITRE);

var result = volume.ConvertTo(VolumeUnit.MILLILITRE);
```

**Result:**

```
1000 ml
```

## 🧪 Testing

The project includes NUnit based unit tests covering all use cases.

### Run tests

```bash
dotnet test
```

### Test coverage includes

- ✔ Length equality
- ✔ Length conversion
- ✔ Length arithmetic
- ✔ Weight equality
- ✔ Weight conversion
- ✔ Volume operations
- ✔ Subtraction
- ✔ Division

## 📂 Project Structure

```
quantitymeasurementapp
│
├── QuantityMeasurementApp
│
│   ├── Program.cs
│   ├── Enums
│   │   ├── LengthUnit.cs
│   │   ├── WeightUnit.cs
│   │   ├── VolumeUnit.cs
│   │   └── UnitType.cs
│   │
│   ├── Extensions
│   │   └── VolumeUnitExtensions.cs
│   │
│   ├── Interfaces
│   │   └── IMeasurable.cs
│   │
│   ├── Models
│   │   ├── Length.cs
│   │   ├── Weight.cs
│   │   └── Quantity.cs
│   │
│   └── Services
│       └── QuantityMeasurementApp.cs
│
└── QuantityMeasurementApp.Tests
    └── QuantityMeasurementTests.cs
```

## 🌳 Branch Strategy

The repository follows a structured Git workflow.

### main

- Production-ready stable code

### develop

- Integration branch
- All feature branches merge here first

### feature branches

- Used for implementing specific use cases
- Example:
  - `feature/UC1-length-equality`
  - `feature/UC6-length-addition`
  - `feature/UC11-volume-support`

### Development flow

```
feature → develop → main
```

## 🤝 Contributing

1️⃣ **Fork the repository**

2️⃣ **Create a feature branch**

```bash
git checkout -b feature/new-feature
```

3️⃣ **Commit changes**

```bash
git commit -m "Add new feature"
```

4️⃣ **Push branch**

```bash
git push origin feature/new-feature
```

5️⃣ **Create Pull Request**

## 📜 License

This project is developed for educational and learning purposes." 
