using System;
using LabWork.Models;

namespace LabWork.Adapters
{
    /// <summary>
    /// Фасад для управління різними типами залізничних колій
    /// Спрощує роботу з адаптерами та дотримується інкапсуляції
    /// Повертає дані/об'єкти, а не самостійно виконує виведення
    /// </summary>
    public class RailwayAdapterFacade
    {
        private readonly IEuropeanRailway _europeanRailway;
        private readonly IWideGaugeRailway _wideGaugeRailway;

        /// <summary>
        /// Ініціалізує фасад залізничних адаптерів
        /// </summary>
        /// <param name="europeanRailway">Європейська залізниця</param>
        /// <param name="wideGaugeRailway">Ширококолійна залізниця</param>
        /// <exception cref="ArgumentNullException">Якщо будь-який параметр є null</exception>
        public RailwayAdapterFacade(IEuropeanRailway europeanRailway, IWideGaugeRailway wideGaugeRailway)
        {
            _europeanRailway = europeanRailway ?? throw new ArgumentNullException(nameof(europeanRailway));
            _wideGaugeRailway = wideGaugeRailway ?? throw new ArgumentNullException(nameof(wideGaugeRailway));
        }

        /// <summary>
        /// Отримує оригінальну європейську залізницю
        /// </summary>
        public IEuropeanRailway GetEuropeanRailway() => _europeanRailway;

        /// <summary>
        /// Отримує оригінальну ширококолійну залізницю
        /// </summary>
        public IWideGaugeRailway GetWideGaugeRailway() => _wideGaugeRailway;

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
        /// Повертає структуроване представлення даних
        /// </summary>
        public RailwayComparison GetComparison()
        {
            return new RailwayComparison
            {
                EuropeanRailway = new RailwayInfo
                {
                    Width = _europeanRailway.GetEuropeanGaugeWidth(),
                    Standard = _europeanRailway.GetEuropeanStandard(),
                    Description = _europeanRailway.GetEuropeanDescription()
                },
                WideGaugeRailway = new RailwayInfo
                {
                    Width = _wideGaugeRailway.GetWideGaugeWidth(),
                    Standard = _wideGaugeRailway.GetWideGaugeStandard(),
                    Description = _wideGaugeRailway.GetWideGaugeDescription()
                },
                AdaptedEuropeanAsWide = new RailwayInfo
                {
                    Width = GetEuropeanAsWideGauge().GetWideGaugeWidth(),
                    Standard = GetEuropeanAsWideGauge().GetWideGaugeStandard(),
                    Description = GetEuropeanAsWideGauge().GetWideGaugeDescription()
                },
                AdaptedWideAsEuropean = new RailwayInfo
                {
                    Width = GetWideGaugeAsEuropean().GetEuropeanGaugeWidth(),
                    Standard = GetWideGaugeAsEuropean().GetEuropeanStandard(),
                    Description = GetWideGaugeAsEuropean().GetEuropeanDescription()
                }
            };
        }
    }

    /// <summary>
    /// Структура для представлення інформації про залізницю
    /// </summary>
    public class RailwayInfo
    {
        /// <summary>
        /// Ширина колії в мм
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Стандарт колії
        /// </summary>
        public string Standard { get; set; }

        /// <summary>
        /// Описання колії
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Структура для представлення порівняння залізниць
    /// </summary>
    public class RailwayComparison
    {
        /// <summary>
        /// Інформація про європейську залізницю
        /// </summary>
        public RailwayInfo EuropeanRailway { get; set; }

        /// <summary>
        /// Інформація про ширококолійну залізницю
        /// </summary>
        public RailwayInfo WideGaugeRailway { get; set; }

        /// <summary>
        /// Інформація про адаптовану європейську залізницю як ширококолійну
        /// </summary>
        public RailwayInfo AdaptedEuropeanAsWide { get; set; }

        /// <summary>
        /// Інформація про адаптовану ширококолійну залізницю як європейську
        /// </summary>
        public RailwayInfo AdaptedWideAsEuropean { get; set; }
    }
}
