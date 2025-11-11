using System;
using LabWork.Models;
using LabWork.Adapters;
using LabWork.Tests;

namespace LabWork
{
    /// <summary>
    /// Основна програма для демонстрації паттерну Adapter
    /// Демонструє конвертацію між європейськими та ширококолійними залізницями
    /// Фасад повертає дані, а не самостійно їх виводить
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Якщо передано аргумент "test", запускаємо тести
            if (args.Length > 0 && args[0].ToLower() == "test")
            {
                RailwayAdapterTests.RunAllTests();
                Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
                Console.ReadKey();
                return;
            }

            // Демонстрація основної функціональності
            RunDemo();
        }

        /// <summary>
        /// Демонстрація основної функціональності адаптерів
        /// </summary>
        static void RunDemo()
        {
            // Створення конкретних об'єктів залізниць
            IEuropeanRailway europeanRailway = new EuropeanRailway();
            IWideGaugeRailway wideGaugeRailway = new WideGaugeRailway();

            // Створення фасаду для управління адаптерами
            RailwayAdapterFacade railwayFacade = new RailwayAdapterFacade(europeanRailway, wideGaugeRailway);

            // Отримання і виведення порівняння залізниць та адаптованих версій
            DisplayComparison(railwayFacade);

            Console.WriteLine("\n=== Демонстрація адаптерів ===\n");

            // Демонстрація прямого використання адаптерів
            DemonstrateEuropeanToWideGaugeAdapter(railwayFacade);
            Console.WriteLine();
            DemonstrateWideToEuropeanGaugeAdapter(railwayFacade);

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }

        /// <summary>
        /// Виводить порівняння залізниць з отриманих даних від фасаду
        /// </summary>
        private static void DisplayComparison(RailwayAdapterFacade railwayFacade)
        {
            var comparison = railwayFacade.GetComparison();

            Console.WriteLine("=== Порівняння залізничних колій ===\n");

            DisplayRailwayInfo("Європейська колія", comparison.EuropeanRailway);
            DisplayRailwayInfo("Ширококолійна колія", comparison.WideGaugeRailway);
            DisplayRailwayInfo("Адаптована європейська колія (як ширококолійна)", comparison.AdaptedEuropeanAsWide);
            DisplayRailwayInfo("Адаптована ширококолійна колія (як європейська)", comparison.AdaptedWideAsEuropean);
        }

        /// <summary>
        /// Допоміжний метод для виведення інформації про залізницю
        /// </summary>
        private static void DisplayRailwayInfo(string title, RailwayInfo info)
        {
            Console.WriteLine($"{title}:");
            Console.WriteLine($"  Ширина: {info.Width} мм");
            Console.WriteLine($"  Стандарт: {info.Standard}");
            Console.WriteLine($"  Опис: {info.Description}\n");
        }

        /// <summary>
        /// Демонструє конвертацію європейської колії у ширококолійну
        /// </summary>
        private static void DemonstrateEuropeanToWideGaugeAdapter(RailwayAdapterFacade railwayFacade)
        {
            Console.WriteLine("1. Адаптер: Європейська → Ширококолійна");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            var europeanRailway = railwayFacade.GetEuropeanRailway();
            var adapter = railwayFacade.GetEuropeanAsWideGauge();

            Console.WriteLine($"Оригінальна європейська ширина: {europeanRailway.GetEuropeanGaugeWidth()} мм");
            Console.WriteLine($"Адаптована ширина: {adapter.GetWideGaugeWidth()} мм");
            Console.WriteLine($"Різниця конверсії: {GaugeConverter.GaugeConversionDifference} мм");
            Console.WriteLine($"Стандарт: {adapter.GetWideGaugeStandard()}");
        }

        /// <summary>
        /// Демонструє конвертацію ширококолійної колії у європейську
        /// </summary>
        private static void DemonstrateWideToEuropeanGaugeAdapter(RailwayAdapterFacade railwayFacade)
        {
            Console.WriteLine("2. Адаптер: Ширококолійна → Європейська");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            var wideGaugeRailway = railwayFacade.GetWideGaugeRailway();
            var adapter = railwayFacade.GetWideGaugeAsEuropean();

            Console.WriteLine($"Оригінальна ширококолійна ширина: {wideGaugeRailway.GetWideGaugeWidth()} мм");
            Console.WriteLine($"Адаптована ширина: {adapter.GetEuropeanGaugeWidth()} мм");
            Console.WriteLine($"Різниця конверсії: {GaugeConverter.GaugeConversionDifference} мм");
            Console.WriteLine($"Стандарт: {adapter.GetEuropeanStandard()}");
        }
    }
}
