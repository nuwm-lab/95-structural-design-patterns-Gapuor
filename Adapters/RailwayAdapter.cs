using System;
using LabWork.Models;

namespace LabWork.Adapters
{
    /// <summary>
    /// Адаптер для конвертації між європейськими та ширококолійними залізницями
    /// Реалізує паттерн Adapter для інтеграції несумісних інтерфейсів
    /// </summary>
    public class EuropeanToWideGaugeAdapter : IWideGaugeRailway
    {
        private readonly IEuropeanRailway _europeanRailway;

        /// <summary>
        /// Ініціалізує адаптер з європейською залізницею
        /// </summary>
        /// <param name="europeanRailway">Об'єкт європейської залізниці</param>
        public EuropeanToWideGaugeAdapter(IEuropeanRailway europeanRailway)
        {
            _europeanRailway = europeanRailway ?? throw new ArgumentNullException(nameof(europeanRailway));
        }

        /// <summary>
        /// Конвертує ширину європейської колії у ширококолійний формат
        /// Додає різницю між стандартами (241 мм)
        /// </summary>
        public int GetWideGaugeWidth()
        {
            const int gaugeConversionDifference = 241; // мм - різниця між стандартами
            return _europeanRailway.GetEuropeanGaugeWidth() + gaugeConversionDifference;
        }

        /// <summary>
        /// Адаптує європейський стандарт у ширококолійний
        /// </summary>
        public string GetWideGaugeStandard()
        {
            return $"Адаптований {_europeanRailway.GetEuropeanStandard()} до ширококолійної системи";
        }

        /// <summary>
        /// Адаптує описання європейської колії
        /// </summary>
        public string GetWideGaugeDescription()
        {
            return $"{_europeanRailway.GetEuropeanDescription()} (адаптована для ширококолійної системи)";
        }
    }

    /// <summary>
    /// Зворотний адаптер для конвертації від ширококолійних до європейських залізниць
    /// </summary>
    public class WideToEuropeanGaugeAdapter : IEuropeanRailway
    {
        private readonly IWideGaugeRailway _wideGaugeRailway;

        /// <summary>
        /// Ініціалізує адаптер з ширококолійною залізницею
        /// </summary>
        /// <param name="wideGaugeRailway">Об'єкт ширококолійної залізниці</param>
        public WideToEuropeanGaugeAdapter(IWideGaugeRailway wideGaugeRailway)
        {
            _wideGaugeRailway = wideGaugeRailway ?? throw new ArgumentNullException(nameof(wideGaugeRailway));
        }

        /// <summary>
        /// Конвертує ширину ширококолійної колії у європейський формат
        /// </summary>
        public int GetEuropeanGaugeWidth()
        {
            const int gaugeConversionDifference = 241; // мм
            int wideWidth = _wideGaugeRailway.GetWideGaugeWidth();
            return wideWidth > gaugeConversionDifference ? wideWidth - gaugeConversionDifference : 0;
        }

        /// <summary>
        /// Адаптує ширококолійний стандарт у європейський
        /// </summary>
        public string GetEuropeanStandard()
        {
            return $"Адаптований {_wideGaugeRailway.GetWideGaugeStandard()} до європейської системи";
        }

        /// <summary>
        /// Адаптує описання ширококолійної колії
        /// </summary>
        public string GetEuropeanDescription()
        {
            return $"{_wideGaugeRailway.GetWideGaugeDescription()} (адаптована для європейської системи)";
        }
    }

    /// <summary>
    /// Фасад для управління різними типами залізничних колій
    /// Спрощує роботу з адаптерами та дотримується інкапсуляції
    /// </summary>
    public class RailwayAdapter
    {
        private readonly IEuropeanRailway _europeanRailway;
        private readonly IWideGaugeRailway _wideGaugeRailway;

        /// <summary>
        /// Ініціалізує фасад залізничних адаптерів
        /// </summary>
        /// <param name="europeanRailway">Європейська залізниця</param>
        /// <param name="wideGaugeRailway">Ширококолійна залізниця</param>
        public RailwayAdapter(IEuropeanRailway europeanRailway, IWideGaugeRailway wideGaugeRailway)
        {
            _europeanRailway = europeanRailway ?? throw new ArgumentNullException(nameof(europeanRailway));
            _wideGaugeRailway = wideGaugeRailway ?? throw new ArgumentNullException(nameof(wideGaugeRailway));
        }

        /// <summary>
        /// Отримує адаптований об'єкт європейської колії як ширококолійна
        /// </summary>
        public IWideGaugeRailway GetEuropeanAsWideGauge()
        {
            return new EuropeanToWideGaugeAdapter(_europeanRailway);
        }

        /// <summary>
        /// Отримує адаптований об'єкт ширококолійної колії як європейська
        /// </summary>
        public IEuropeanRailway GetWideGaugeAsEuropean()
        {
            return new WideToEuropeanGaugeAdapter(_wideGaugeRailway);
        }

        /// <summary>
        /// Порівнює характеристики двох типів колій
        /// </summary>
        public void CompareRailways()
        {
            Console.WriteLine("=== Порівняння залізничних колій ===\n");

            Console.WriteLine("Європейська колія:");
            Console.WriteLine($"  Ширина: {_europeanRailway.GetEuropeanGaugeWidth()} мм");
            Console.WriteLine($"  Стандарт: {_europeanRailway.GetEuropeanStandard()}");
            Console.WriteLine($"  Опис: {_europeanRailway.GetEuropeanDescription()}\n");

            Console.WriteLine("Ширококолійна колія:");
            Console.WriteLine($"  Ширина: {_wideGaugeRailway.GetWideGaugeWidth()} мм");
            Console.WriteLine($"  Стандарт: {_wideGaugeRailway.GetWideGaugeStandard()}");
            Console.WriteLine($"  Опис: {_wideGaugeRailway.GetWideGaugeDescription()}\n");

            Console.WriteLine("Адаптована європейська колія (як ширококолійна):");
            var adaptedEuropean = GetEuropeanAsWideGauge();
            Console.WriteLine($"  Ширина: {adaptedEuropean.GetWideGaugeWidth()} мм");
            Console.WriteLine($"  Стандарт: {adaptedEuropean.GetWideGaugeStandard()}");
            Console.WriteLine($"  Опис: {adaptedEuropean.GetWideGaugeDescription()}\n");

            Console.WriteLine("Адаптована ширококолійна колія (як європейська):");
            var adaptedWide = GetWideGaugeAsEuropean();
            Console.WriteLine($"  Ширина: {adaptedWide.GetEuropeanGaugeWidth()} мм");
            Console.WriteLine($"  Стандарт: {adaptedWide.GetEuropeanStandard()}");
            Console.WriteLine($"  Опис: {adaptedWide.GetEuropeanDescription()}");
        }
    }
}
