using System;
using LabWork.Models;

namespace LabWork.Adapters
{
    /// <summary>
    /// Адаптер для конвертації європейської залізниці у ширококолійну
    /// Реалізує паттерн Adapter для інтеграції несумісних інтерфейсів
    /// </summary>
    public class EuropeanToWideGaugeAdapter : IWideGaugeRailway
    {
        private readonly IEuropeanRailway _europeanRailway;

        /// <summary>
        /// Ініціалізує адаптер з європейською залізницею
        /// </summary>
        /// <param name="europeanRailway">Об'єкт європейської залізниці</param>
        /// <exception cref="ArgumentNullException">Якщо europeanRailway є null</exception>
        public EuropeanToWideGaugeAdapter(IEuropeanRailway europeanRailway)
        {
            _europeanRailway = europeanRailway ?? throw new ArgumentNullException(nameof(europeanRailway));
        }

        /// <summary>
        /// Конвертує ширину європейської колії у ширококолійний формат
        /// </summary>
        public int GetWideGaugeWidth()
        {
            return GaugeConverter.ConvertEuropeanToWideGauge(_europeanRailway.GetEuropeanGaugeWidth());
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
}
