using BusinessLayer.Services;
using BusinessLayer.Interfaces;
using BusinessLayer.Exceptions;
using ModelLayer.DTOs;
using ModelLayer.Enums;
using QuantityMeasurementApp.Controllers;
using RepositoryLayer.Implementations;
using RepositoryLayer.Interfaces;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        private static IQuantityMeasurementRepository? _repository;
        private static IQuantityMeasurementService? _service;
        private static QuantityMeasurementController? _controller;

        static void Main(string[] args)
        {
            InitializeDependencies();
            
            bool exit = false;
            
            while (!exit)
            {
                Console.Clear();
                DisplayHeader();
                DisplayMainMenu();
                
                string choice = Console.ReadLine() ?? "";
                
                switch (choice)
                {
                    case "1":
                        RunLengthOperations();
                        break;
                    case "2":
                        RunWeightOperations();
                        break;
                    case "3":
                        RunVolumeOperations();
                        break;
                    case "4":
                        RunTemperatureOperations();
                        break;
                    case "5":
                        RunGenericQuantityOperations();
                        break;
                    case "6":
                        RunCustomOperations();
                        break;
                    case "7":
                        RunAllDemos();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Thank you for using Quantity Measurement App. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void InitializeDependencies()
        {
            _repository = QuantityMeasurementCacheRepository.Instance;
            _service = new QuantityMeasurementService(_repository);
            _controller = new QuantityMeasurementController(_service);
        }

        static void DisplayHeader()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   QUANTITY MEASUREMENT APP");
            Console.WriteLine("========================================");
            Console.WriteLine();
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("MAIN MENU:");
            Console.WriteLine("==========");
            Console.WriteLine("1. Length Operations");
            Console.WriteLine("2. Weight Operations");
            Console.WriteLine("3. Volume Operations");
            Console.WriteLine("4. Temperature Operations");
            Console.WriteLine("5. Generic Quantity Operations");
            Console.WriteLine("6. Custom Operations (Enter Your Own Values)");
            Console.WriteLine("7. Run All Demonstrations");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.Write("Enter your choice: ");
        }

        static void WaitForKeyPress()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static MeasurementRequest CreateRequest(double value, string unit, string type)
        {
            return new MeasurementRequest
            {
                Value = value,
                Unit = unit,
                Type = type
            };
        }

        #region Length Operations

        static void RunLengthOperations()
        {
            bool back = false;
            
            while (!back)
            {
                Console.Clear();
                DisplayHeader();
                Console.WriteLine("LENGTH OPERATIONS");
                Console.WriteLine("=================");
                Console.WriteLine("1. Demonstrate Equality (12 inches vs 1 foot)");
                Console.WriteLine("2. Demonstrate Conversion (12 inches to feet)");
                Console.WriteLine("3. Demonstrate Addition (12 inches + 1 foot)");
                Console.WriteLine("4. Custom Length Operations");
                Console.WriteLine("0. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        DemonstrateLengthEquality();
                        break;
                    case "2":
                        DemonstrateLengthConversion();
                        break;
                    case "3":
                        DemonstrateLengthAddition();
                        break;
                    case "4":
                        RunCustomLengthOperations();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
                
                if (choice != "0")
                    WaitForKeyPress();
            }
        }

        static void DemonstrateLengthEquality()
        {
            try
            {
                var req1 = CreateRequest(12, nameof(LengthUnit.INCHES), "LENGTH");
                var req2 = CreateRequest(1, nameof(LengthUnit.FEET), "LENGTH");
                _controller?.DemonstrateComparison(req1, req2);
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DemonstrateLengthConversion()
        {
            try
            {
                var req = CreateRequest(12, nameof(LengthUnit.INCHES), "LENGTH");
                _controller?.DemonstrateConversion(req, nameof(LengthUnit.FEET));
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DemonstrateLengthAddition()
        {
            try
            {
                var req1 = CreateRequest(12, nameof(LengthUnit.INCHES), "LENGTH");
                var req2 = CreateRequest(1, nameof(LengthUnit.FEET), "LENGTH");
                _controller?.DemonstrateAddition(req1, req2);
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        #endregion

        #region Weight Operations

        static void RunWeightOperations()
        {
            bool back = false;
            
            while (!back)
            {
                Console.Clear();
                DisplayHeader();
                Console.WriteLine("WEIGHT OPERATIONS");
                Console.WriteLine("=================");
                Console.WriteLine("1. Demonstrate Equality (1000 grams vs 1 kilogram)");
                Console.WriteLine("2. Demonstrate Conversion (2.2 pounds to kilograms)");
                Console.WriteLine("3. Demonstrate Addition (1000 grams + 1 kilogram)");
                Console.WriteLine("4. Custom Weight Operations");
                Console.WriteLine("0. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        DemonstrateWeightEquality();
                        break;
                    case "2":
                        DemonstrateWeightConversion();
                        break;
                    case "3":
                        DemonstrateWeightAddition();
                        break;
                    case "4":
                        RunCustomWeightOperations();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
                
                if (choice != "0")
                    WaitForKeyPress();
            }
        }

        static void DemonstrateWeightEquality()
        {
            try
            {
                var req1 = CreateRequest(1000, nameof(WeightUnit.GRAM), "WEIGHT");
                var req2 = CreateRequest(1, nameof(WeightUnit.KILOGRAM), "WEIGHT");
                _controller?.DemonstrateComparison(req1, req2);
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DemonstrateWeightConversion()
        {
            try
            {
                var req = CreateRequest(2.2, nameof(WeightUnit.POUND), "WEIGHT");
                _controller?.DemonstrateConversion(req, nameof(WeightUnit.KILOGRAM));
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DemonstrateWeightAddition()
        {
            try
            {
                var req1 = CreateRequest(1000, nameof(WeightUnit.GRAM), "WEIGHT");
                var req2 = CreateRequest(1, nameof(WeightUnit.KILOGRAM), "WEIGHT");
                _controller?.DemonstrateAddition(req1, req2);
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        #endregion

        #region Volume Operations

        static void RunVolumeOperations()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("VOLUME OPERATIONS");
            Console.WriteLine("=================");
            
            try
            {
                var req1 = CreateRequest(1, nameof(VolumeUnit.LITRE), "VOLUME");
                var req2 = CreateRequest(1000, nameof(VolumeUnit.MILLILITRE), "VOLUME");
                var req3 = CreateRequest(1, nameof(VolumeUnit.GALLON), "VOLUME");
                
                Console.WriteLine("--- VOLUME COMPARISON ---");
                _controller?.DemonstrateComparison(req1, req2);
                
                Console.WriteLine("\n--- VOLUME CONVERSION ---");
                _controller?.DemonstrateConversion(req3, nameof(VolumeUnit.LITRE));
                
                Console.WriteLine("\n--- VOLUME ADDITION ---");
                _controller?.DemonstrateAddition(req1, req2);
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        #endregion

        #region Temperature Operations

        static void RunTemperatureOperations()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("TEMPERATURE OPERATIONS");
            Console.WriteLine("======================");
            
            try
            {
                var req1 = CreateRequest(100, nameof(TemperatureUnit.CELSIUS), "TEMPERATURE");
                var req2 = CreateRequest(212, nameof(TemperatureUnit.FAHRENHEIT), "TEMPERATURE");
                
                Console.WriteLine("--- TEMPERATURE COMPARISON ---");
                _controller?.DemonstrateComparison(req1, req2);
                
                Console.WriteLine("\n--- TEMPERATURE CONVERSION ---");
                _controller?.DemonstrateConversion(req1, nameof(TemperatureUnit.FAHRENHEIT));
                
                Console.WriteLine("\n--- TEMPERATURE ADDITION (Should Fail) ---");
                _controller?.DemonstrateAddition(req1, req2);
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Expected error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        #endregion

        #region Generic Quantity Operations

        static void RunGenericQuantityOperations()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("GENERIC QUANTITY OPERATIONS");
            Console.WriteLine("===========================");
            
            try
            {
                var lengthReq1 = CreateRequest(36, nameof(LengthUnit.INCHES), "LENGTH");
                var lengthReq2 = CreateRequest(1, nameof(LengthUnit.YARDS), "LENGTH");
                
                Console.WriteLine("--- GENERIC LENGTH COMPARISON ---");
                _controller?.DemonstrateComparison(lengthReq1, lengthReq2);
                
                var weightReq1 = CreateRequest(500, nameof(WeightUnit.GRAM), "WEIGHT");
                var weightReq2 = CreateRequest(0.5, nameof(WeightUnit.KILOGRAM), "WEIGHT");
                
                Console.WriteLine("\n--- GENERIC WEIGHT COMPARISON ---");
                _controller?.DemonstrateComparison(weightReq1, weightReq2);
                
                Console.WriteLine("\n--- GENERIC WEIGHT ADDITION ---");
                _controller?.DemonstrateAddition(weightReq1, weightReq2);
            }
            catch (QuantityMeasurementException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        #endregion

        #region All Demonstrations

        static void RunAllDemos()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("RUNNING ALL DEMONSTRATIONS");
            Console.WriteLine("===========================");
            Console.WriteLine();
            
            DemonstrateLengthEquality();
            DemonstrateLengthConversion();
            DemonstrateLengthAddition();
            
            Console.WriteLine();
            DemonstrateWeightEquality();
            DemonstrateWeightConversion();
            DemonstrateWeightAddition();
            
            Console.WriteLine();
            RunVolumeOperations();
            
            Console.WriteLine();
            RunTemperatureOperations();
            
            WaitForKeyPress();
        }

        #endregion

        #region Custom Operations

        static void RunCustomOperations()
        {
            bool back = false;
            
            while (!back)
            {
                Console.Clear();
                DisplayHeader();
                Console.WriteLine("CUSTOM OPERATIONS");
                Console.WriteLine("=================");
                Console.WriteLine("1. Length Operations");
                Console.WriteLine("2. Weight Operations");
                Console.WriteLine("3. Volume Operations");
                Console.WriteLine("4. Temperature Operations");
                Console.WriteLine("0. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        RunCustomLengthOperations();
                        break;
                    case "2":
                        RunCustomWeightOperations();
                        break;
                    case "3":
                        RunCustomVolumeOperations();
                        break;
                    case "4":
                        RunCustomTemperatureOperations();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        WaitForKeyPress();
                        break;
                }
            }
        }

        static void RunCustomLengthOperations()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("CUSTOM LENGTH OPERATIONS");
            Console.WriteLine("========================");
            
            try
            {
                Console.Write("Enter value 1: ");
                double val1 = double.Parse(Console.ReadLine() ?? "0");
                
                Console.WriteLine("Select unit 1:");
                Console.WriteLine("1. FEET");
                Console.WriteLine("2. INCHES");
                Console.WriteLine("3. YARDS");
                Console.WriteLine("4. CENTIMETERS");
                Console.Write("Choice: ");
                string unit1Choice = Console.ReadLine() ?? "1";
                
                string unit1 = unit1Choice switch
                {
                    "1" => nameof(LengthUnit.FEET),
                    "2" => nameof(LengthUnit.INCHES),
                    "3" => nameof(LengthUnit.YARDS),
                    "4" => nameof(LengthUnit.CENTIMETERS),
                    _ => nameof(LengthUnit.FEET)
                };
                
                Console.Write("Enter value 2: ");
                double val2 = double.Parse(Console.ReadLine() ?? "0");
                
                Console.WriteLine("Select unit 2:");
                Console.WriteLine("1. FEET");
                Console.WriteLine("2. INCHES");
                Console.WriteLine("3. YARDS");
                Console.WriteLine("4. CENTIMETERS");
                Console.Write("Choice: ");
                string unit2Choice = Console.ReadLine() ?? "1";
                
                string unit2 = unit2Choice switch
                {
                    "1" => nameof(LengthUnit.FEET),
                    "2" => nameof(LengthUnit.INCHES),
                    "3" => nameof(LengthUnit.YARDS),
                    "4" => nameof(LengthUnit.CENTIMETERS),
                    _ => nameof(LengthUnit.FEET)
                };
                
                var req1 = CreateRequest(val1, unit1, "LENGTH");
                var req2 = CreateRequest(val2, unit2, "LENGTH");
                
                Console.WriteLine("\nRESULTS:");
                _controller?.DemonstrateComparison(req1, req2);
                
                Console.Write("\nEnter target unit for conversion (1-4): ");
                string targetChoice = Console.ReadLine() ?? "1";
                string targetUnit = targetChoice switch
                {
                    "1" => nameof(LengthUnit.FEET),
                    "2" => nameof(LengthUnit.INCHES),
                    "3" => nameof(LengthUnit.YARDS),
                    "4" => nameof(LengthUnit.CENTIMETERS),
                    _ => nameof(LengthUnit.FEET)
                };
                
                _controller?.DemonstrateConversion(req1, targetUnit);
                
                Console.WriteLine("\nAddition Result:");
                _controller?.DemonstrateAddition(req1, req2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        static void RunCustomWeightOperations()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("CUSTOM WEIGHT OPERATIONS");
            Console.WriteLine("========================");
            
            try
            {
                Console.Write("Enter value 1: ");
                double val1 = double.Parse(Console.ReadLine() ?? "0");
                
                Console.WriteLine("Select unit 1:");
                Console.WriteLine("1. MILLIGRAM");
                Console.WriteLine("2. GRAM");
                Console.WriteLine("3. KILOGRAM");
                Console.WriteLine("4. POUND");
                Console.WriteLine("5. TONNE");
                Console.Write("Choice: ");
                string unit1Choice = Console.ReadLine() ?? "2";
                
                string unit1 = unit1Choice switch
                {
                    "1" => nameof(WeightUnit.MILLIGRAM),
                    "2" => nameof(WeightUnit.GRAM),
                    "3" => nameof(WeightUnit.KILOGRAM),
                    "4" => nameof(WeightUnit.POUND),
                    "5" => nameof(WeightUnit.TONNE),
                    _ => nameof(WeightUnit.GRAM)
                };
                
                Console.Write("Enter value 2: ");
                double val2 = double.Parse(Console.ReadLine() ?? "0");
                
                Console.WriteLine("Select unit 2:");
                Console.WriteLine("1. MILLIGRAM");
                Console.WriteLine("2. GRAM");
                Console.WriteLine("3. KILOGRAM");
                Console.WriteLine("4. POUND");
                Console.WriteLine("5. TONNE");
                Console.Write("Choice: ");
                string unit2Choice = Console.ReadLine() ?? "2";
                
                string unit2 = unit2Choice switch
                {
                    "1" => nameof(WeightUnit.MILLIGRAM),
                    "2" => nameof(WeightUnit.GRAM),
                    "3" => nameof(WeightUnit.KILOGRAM),
                    "4" => nameof(WeightUnit.POUND),
                    "5" => nameof(WeightUnit.TONNE),
                    _ => nameof(WeightUnit.GRAM)
                };
                
                var req1 = CreateRequest(val1, unit1, "WEIGHT");
                var req2 = CreateRequest(val2, unit2, "WEIGHT");
                
                Console.WriteLine("\nRESULTS:");
                _controller?.DemonstrateComparison(req1, req2);
                
                Console.Write("\nEnter target unit for conversion (1-5): ");
                string targetChoice = Console.ReadLine() ?? "2";
                string targetUnit = targetChoice switch
                {
                    "1" => nameof(WeightUnit.MILLIGRAM),
                    "2" => nameof(WeightUnit.GRAM),
                    "3" => nameof(WeightUnit.KILOGRAM),
                    "4" => nameof(WeightUnit.POUND),
                    "5" => nameof(WeightUnit.TONNE),
                    _ => nameof(WeightUnit.GRAM)
                };
                
                _controller?.DemonstrateConversion(req1, targetUnit);
                
                Console.WriteLine("\nAddition Result:");
                _controller?.DemonstrateAddition(req1, req2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        static void RunCustomVolumeOperations()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("CUSTOM VOLUME OPERATIONS");
            Console.WriteLine("========================");
            
            try
            {
                Console.Write("Enter value 1: ");
                double val1 = double.Parse(Console.ReadLine() ?? "0");
                
                Console.WriteLine("Select unit 1:");
                Console.WriteLine("1. LITRE");
                Console.WriteLine("2. MILLILITRE");
                Console.WriteLine("3. GALLON");
                Console.Write("Choice: ");
                string unit1Choice = Console.ReadLine() ?? "1";
                
                string unit1 = unit1Choice switch
                {
                    "1" => nameof(VolumeUnit.LITRE),
                    "2" => nameof(VolumeUnit.MILLILITRE),
                    "3" => nameof(VolumeUnit.GALLON),
                    _ => nameof(VolumeUnit.LITRE)
                };
                
                Console.Write("Enter value 2: ");
                double val2 = double.Parse(Console.ReadLine() ?? "0");
                
                Console.WriteLine("Select unit 2:");
                Console.WriteLine("1. LITRE");
                Console.WriteLine("2. MILLILITRE");
                Console.WriteLine("3. GALLON");
                Console.Write("Choice: ");
                string unit2Choice = Console.ReadLine() ?? "1";
                
                string unit2 = unit2Choice switch
                {
                    "1" => nameof(VolumeUnit.LITRE),
                    "2" => nameof(VolumeUnit.MILLILITRE),
                    "3" => nameof(VolumeUnit.GALLON),
                    _ => nameof(VolumeUnit.LITRE)
                };
                
                var req1 = CreateRequest(val1, unit1, "VOLUME");
                var req2 = CreateRequest(val2, unit2, "VOLUME");
                
                Console.WriteLine("\nRESULTS:");
                _controller?.DemonstrateComparison(req1, req2);
                
                Console.Write("\nEnter target unit for conversion (1-3): ");
                string targetChoice = Console.ReadLine() ?? "1";
                string targetUnit = targetChoice switch
                {
                    "1" => nameof(VolumeUnit.LITRE),
                    "2" => nameof(VolumeUnit.MILLILITRE),
                    "3" => nameof(VolumeUnit.GALLON),
                    _ => nameof(VolumeUnit.LITRE)
                };
                
                _controller?.DemonstrateConversion(req1, targetUnit);
                
                Console.WriteLine("\nAddition Result:");
                _controller?.DemonstrateAddition(req1, req2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        static void RunCustomTemperatureOperations()
        {
            Console.Clear();
            DisplayHeader();
            Console.WriteLine("CUSTOM TEMPERATURE OPERATIONS");
            Console.WriteLine("=============================");
            
            try
            {
                Console.Write("Enter value 1: ");
                double val1 = double.Parse(Console.ReadLine() ?? "0");
                
                Console.WriteLine("Select unit 1:");
                Console.WriteLine("1. CELSIUS");
                Console.WriteLine("2. FAHRENHEIT");
                Console.Write("Choice: ");
                string unit1Choice = Console.ReadLine() ?? "1";
                
                string unit1 = unit1Choice == "1" ? nameof(TemperatureUnit.CELSIUS) : nameof(TemperatureUnit.FAHRENHEIT);
                
                Console.Write("Enter value 2: ");
                double val2 = double.Parse(Console.ReadLine() ?? "0");
                
                Console.WriteLine("Select unit 2:");
                Console.WriteLine("1. CELSIUS");
                Console.WriteLine("2. FAHRENHEIT");
                Console.Write("Choice: ");
                string unit2Choice = Console.ReadLine() ?? "1";
                
                string unit2 = unit2Choice == "1" ? nameof(TemperatureUnit.CELSIUS) : nameof(TemperatureUnit.FAHRENHEIT);
                
                var req1 = CreateRequest(val1, unit1, "TEMPERATURE");
                var req2 = CreateRequest(val2, unit2, "TEMPERATURE");
                
                Console.WriteLine("\nRESULTS:");
                _controller?.DemonstrateComparison(req1, req2);
                
                Console.Write("\nConvert first value to (1-Celsius, 2-Fahrenheit): ");
                string convertChoice = Console.ReadLine() ?? "1";
                string targetUnit = convertChoice == "1" ? nameof(TemperatureUnit.CELSIUS) : nameof(TemperatureUnit.FAHRENHEIT);
                
                _controller?.DemonstrateConversion(req1, targetUnit);
                
                Console.WriteLine("\nAttempting addition (should fail):");
                _controller?.DemonstrateAddition(req1, req2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            WaitForKeyPress();
        }

        #endregion
    }
}