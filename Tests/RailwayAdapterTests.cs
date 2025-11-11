using System;
using LabWork.Models;
using LabWork.Adapters;

namespace LabWork.Tests
{
    /// <summary>
    /// Юніт-тести для перевірки функціональності адаптерів
    /// Тестує граничні випадки та коректність конверсії
    /// </summary>
    public class RailwayAdapterTests
    {
        /// <summary>
        /// Тест конверсії європейської колії в ширококолійну
        /// </summary>
        public static void TestEuropeanToWideGaugeAdapter()
        {
            Console.WriteLine("\n=== Тестування EuropeanToWideGaugeAdapter ===\n");

            IEuropeanRailway europeanRailway = new EuropeanRailway();
            var adapter = new EuropeanToWideGaugeAdapter(europeanRailway);

            // Тест 1: Перевірка коректної конверсії ширини
            int expectedWidth = 1435 + 241; // 1676
            int actualWidth = adapter.GetWideGaugeWidth();
            
            Console.WriteLine($"Тест 1 - Конверсія ширини:");
            Console.WriteLine($"  Очікувана ширина: {expectedWidth} мм");
            Console.WriteLine($"  Отримана ширина: {actualWidth} мм");
            Console.WriteLine($"  Результат: {(expectedWidth == actualWidth ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");

            // Тест 2: Перевірка адаптованого стандарту
            string standard = adapter.GetWideGaugeStandard();
            bool standardContainsAdapted = standard.Contains("Адаптований");
            
            Console.WriteLine($"Тест 2 - Адаптований стандарт:");
            Console.WriteLine($"  Стандарт: {standard}");
            Console.WriteLine($"  Результат: {(standardContainsAdapted ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");

            // Тест 3: Перевірка адаптованого описання
            string description = adapter.GetWideGaugeDescription();
            bool descriptionContainsAdapted = description.Contains("адаптована");
            
            Console.WriteLine($"Тест 3 - Адаптоване описання:");
            Console.WriteLine($"  Описання: {description}");
            Console.WriteLine($"  Результат: {(descriptionContainsAdapted ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");
        }

        /// <summary>
        /// Тест конверсії ширококолійної колії в європейську
        /// </summary>
        public static void TestWideToEuropeanGaugeAdapter()
        {
            Console.WriteLine("\n=== Тестування WideToEuropeanGaugeAdapter ===\n");

            IWideGaugeRailway wideGaugeRailway = new WideGaugeRailway();
            var adapter = new WideToEuropeanGaugeAdapter(wideGaugeRailway);

            // Тест 1: Перевірка коректної конверсії ширини
            int expectedWidth = 1676 - 241; // 1435
            int actualWidth = adapter.GetEuropeanGaugeWidth();
            
            Console.WriteLine($"Тест 1 - Конверсія ширини:");
            Console.WriteLine($"  Очікувана ширина: {expectedWidth} мм");
            Console.WriteLine($"  Отримана ширина: {actualWidth} мм");
            Console.WriteLine($"  Результат: {(expectedWidth == actualWidth ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");

            // Тест 2: Перевірка адаптованого стандарту
            string standard = adapter.GetEuropeanStandard();
            bool standardContainsAdapted = standard.Contains("Адаптований");
            
            Console.WriteLine($"Тест 2 - Адаптований стандарт:");
            Console.WriteLine($"  Стандарт: {standard}");
            Console.WriteLine($"  Результат: {(standardContainsAdapted ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");

            // Тест 3: Перевірка адаптованого описання
            string description = adapter.GetEuropeanDescription();
            bool descriptionContainsAdapted = description.Contains("адаптована");
            
            Console.WriteLine($"Тест 3 - Адаптоване описання:");
            Console.WriteLine($"  Описання: {description}");
            Console.WriteLine($"  Результат: {(descriptionContainsAdapted ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");
        }

        /// <summary>
        /// Тест сервісу GaugeConverter для переконання граничних випадків
        /// </summary>
        public static void TestGaugeConverter()
        {
            Console.WriteLine("\n=== Тестування GaugeConverter ===\n");

            // Тест 1: Конверсія європейської в ширококолійну
            int europeanWidth = 1435;
            int convertedWidth = GaugeConverter.ConvertEuropeanToWideGauge(europeanWidth);
            int expectedWidth = 1676;
            
            Console.WriteLine($"Тест 1 - ConvertEuropeanToWideGauge:");
            Console.WriteLine($"  Вхідна ширина: {europeanWidth} мм");
            Console.WriteLine($"  Результат: {convertedWidth} мм");
            Console.WriteLine($"  Очікувана: {expectedWidth} мм");
            Console.WriteLine($"  Результат: {(convertedWidth == expectedWidth ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");

            // Тест 2: Конверсія ширококолійної в європейську
            int wideWidth = 1676;
            int convertedToEuropean = GaugeConverter.ConvertWideGaugeToEuropean(wideWidth);
            int expectedEuropean = 1435;
            
            Console.WriteLine($"Тест 2 - ConvertWideGaugeToEuropean:");
            Console.WriteLine($"  Вхідна ширина: {wideWidth} мм");
            Console.WriteLine($"  Результат: {convertedToEuropean} мм");
            Console.WriteLine($"  Очікувана: {expectedEuropean} мм");
            Console.WriteLine($"  Результат: {(convertedToEuropean == expectedEuropean ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");

            // Тест 3: Граничний випадок - дуже мала ширина для зворотної конверсії
            int tinyWidth = 100;
            int result = GaugeConverter.ConvertWideGaugeToEuropean(tinyWidth);
            
            Console.WriteLine($"Тест 3 - Граничний випадок (від'ємна різниця):");
            Console.WriteLine($"  Вхідна ширина: {tinyWidth} мм");
            Console.WriteLine($"  Результат: {result} мм");
            Console.WriteLine($"  Результат: {(result == 0 ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");

            // Тест 4: Перевірка константи конверсії
            Console.WriteLine($"Тест 4 - Константа конверсії:");
            Console.WriteLine($"  Значення GaugeConversionDifference: {GaugeConverter.GaugeConversionDifference} мм");
            Console.WriteLine($"  Результат: {(GaugeConverter.GaugeConversionDifference == 241 ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");
        }

        /// <summary>
        /// Тест фасаду RailwayAdapterFacade
        /// </summary>
        public static void TestRailwayAdapterFacade()
        {
            Console.WriteLine("\n=== Тестування RailwayAdapterFacade ===\n");

            IEuropeanRailway europeanRailway = new EuropeanRailway();
            IWideGaugeRailway wideGaugeRailway = new WideGaugeRailway();
            var facade = new RailwayAdapterFacade(europeanRailway, wideGaugeRailway);

            // Тест 1: Отримання оригінальних об'єктів
            Console.WriteLine("Тест 1 - Отримання оригінальних об'єктів:");
            var retrievedEuropean = facade.GetEuropeanRailway();
            var retrievedWide = facade.GetWideGaugeRailway();
            
            bool europeanMatch = retrievedEuropean.GetEuropeanGaugeWidth() == 1435;
            bool wideMatch = retrievedWide.GetWideGaugeWidth() == 1676;
            
            Console.WriteLine($"  Європейська: {(europeanMatch ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}");
            Console.WriteLine($"  Ширококолійна: {(wideMatch ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");

            // Тест 2: Отримання адаптованих об'єктів
            Console.WriteLine("Тест 2 - Отримання адаптованих об'єктів:");
            var adaptedEuroAsWide = facade.GetEuropeanAsWideGauge();
            var adaptedWideAsEuro = facade.GetWideGaugeAsEuropean();
            
            bool euroAsWideWorks = adaptedEuroAsWide != null && adaptedEuroAsWide.GetWideGaugeWidth() == 1676;
            bool wideAsEuroWorks = adaptedWideAsEuro != null && adaptedWideAsEuro.GetEuropeanGaugeWidth() == 1435;
            
            Console.WriteLine($"  Європейська як ширококолійна: {(euroAsWideWorks ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}");
            Console.WriteLine($"  Ширokokолійна як європейська: {(wideAsEuroWorks ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");

            // Тест 3: Отримання порівняння
            Console.WriteLine("Тест 3 - Отримання порівняння:");
            var comparison = facade.GetComparison();
            
            bool comparisonValid = comparison != null &&
                                   comparison.EuropeanRailway != null &&
                                   comparison.WideGaugeRailway != null &&
                                   comparison.AdaptedEuropeanAsWide != null &&
                                   comparison.AdaptedWideAsEuropean != null;
            
            Console.WriteLine($"  Структура порівняння: {(comparisonValid ? "✓ ПРОЙДЕНО" : "✗ НЕВДАЧА")}\n");
        }

        /// <summary>
        /// Тест для перевірки інкапсуляції та валідації нульових параметрів
        /// </summary>
        public static void TestEncapsulationAndValidation()
        {
            Console.WriteLine("\n=== Тестування інкапсуляції та валідації ===\n");

            // Тест 1: EuropeanToWideGaugeAdapter з null параметром
            Console.WriteLine("Тест 1 - EuropeanToWideGaugeAdapter з null:");
            try
            {
                var adapter = new EuropeanToWideGaugeAdapter(null);
                Console.WriteLine("  ✗ НЕВДАЧА - очікувалася ArgumentNullException\n");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("  ✓ ПРОЙДЕНО - ArgumentNullException викинута\n");
            }

            // Тест 2: WideToEuropeanGaugeAdapter з null параметром
            Console.WriteLine("Тест 2 - WideToEuropeanGaugeAdapter з null:");
            try
            {
                var adapter = new WideToEuropeanGaugeAdapter(null);
                Console.WriteLine("  ✗ НЕВДАЧА - очікувалася ArgumentNullException\n");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("  ✓ ПРОЙДЕНО - ArgumentNullException викинута\n");
            }

            // Тест 3: RailwayAdapterFacade з null параметрами
            Console.WriteLine("Тест 3 - RailwayAdapterFacade з null:");
            try
            {
                var facade = new RailwayAdapterFacade(null, null);
                Console.WriteLine("  ✗ НЕВДАЧА - очікувалася ArgumentNullException\n");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("  ✓ ПРОЙДЕНО - ArgumentNullException викинута\n");
            }
        }

        /// <summary>
        /// Запускає всі тести
        /// </summary>
        public static void RunAllTests()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║     ЮНІТ-ТЕСТИ ДЛЯ ЗАЛІЗНИЧНИХ АДАПТЕРІВ                      ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");

            TestEuropeanToWideGaugeAdapter();
            TestWideToEuropeanGaugeAdapter();
            TestGaugeConverter();
            TestRailwayAdapterFacade();
            TestEncapsulationAndValidation();

            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   ТЕСТУВАННЯ ЗАВЕРШЕНО                        ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");
        }
    }
}
