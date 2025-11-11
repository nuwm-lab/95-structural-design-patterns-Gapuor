using System;
using LabWork.Models;
using LabWork.Adapters;

namespace LabWork
{
    /// <summary>
    /// Основна програма для демонстрації паттерну Adapter
    /// Демонструє конвертацію між європейськими та ширококолійними залізницями
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Створення конкретних об'єктів залізниць
            IEuropeanRailway europeanRailway = new EuropeanRailway();
            IWideGaugeRailway wideGaugeRailway = new WideGaugeRailway();

            // Створення фасаду для управління адаптерами
            RailwayAdapter railwayAdapter = new RailwayAdapter(europeanRailway, wideGaugeRailway);

            // Виведення порівняння залізниць та адаптованих версій
            railwayAdapter.CompareRailways();

            Console.WriteLine("\n=== Демонстрація адаптерів ===\n");

            // Демонстрація прямого використання адаптерів
            DemonstrateEuropeanToWideGaugeAdapter(europeanRailway);
            Console.WriteLine();
            DemonstrateWideToEuropeanGaugeAdapter(wideGaugeRailway);

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }

        /// <summary>
        /// Демонструє конвертацію європейської колії у ширококолійну
        /// </summary>
        /// <param name="europeanRailway">Європейська залізниця</param>
        private static void DemonstrateEuropeanToWideGaugeAdapter(IEuropeanRailway europeanRailway)
        {
            Console.WriteLine("1. Адаптер: Європейська → Ширококолійна");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            var adapter = new EuropeanToWideGaugeAdapter(europeanRailway);

            Console.WriteLine($"Оригінальна європейська ширина: {europeanRailway.GetEuropeanGaugeWidth()} мм");
            Console.WriteLine($"Адаптована ширина: {adapter.GetWideGaugeWidth()} мм");
            Console.WriteLine($"Стандарт: {adapter.GetWideGaugeStandard()}");
        }

        /// <summary>
        /// Демонструє конвертацію ширококолійної колії у європейську
        /// </summary>
        /// <param name="wideGaugeRailway">Ширококолійна залізниця</param>
        private static void DemonstrateWideToEuropeanGaugeAdapter(IWideGaugeRailway wideGaugeRailway)
        {
            Console.WriteLine("2. Адаптер: Ширококолійна → Європейська");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            var adapter = new WideToEuropeanGaugeAdapter(wideGaugeRailway);

            Console.WriteLine($"Оригінальна ширококолійна ширина: {wideGaugeRailway.GetWideGaugeWidth()} мм");
            Console.WriteLine($"Адаптована ширина: {adapter.GetEuropeanGaugeWidth()} мм");
            Console.WriteLine($"Стандарт: {adapter.GetEuropeanStandard()}");
        }
    }
}
