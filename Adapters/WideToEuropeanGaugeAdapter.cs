using System;
using LabWork.Models;

namespace LabWork.Adapters
{
    /// <summary>
    /// Зворотний адаптер для конвертації від ширококолійних до європейських залізниць
    /// Реалізує паттерн Adapter для інтеграції несумісних інтерфейсів
    /// </summary>
    public class WideToEuropeanGaugeAdapter : IEuropeanRailway
    {
        private readonly IWideGaugeRailway _wideGaugeRailway;

        /// <summary>
        /// Ініціалізує адаптер з ширококолійною залізницею
        /// </summary>
        /// <param name="wideGaugeRailway">Об'єкт ширококолійної залізниці</param>
        /// <exception cref="ArgumentNullException">Якщо wideGaugeRailway є null</exception>
        public WideToEuropeanGaugeAdapter(IWideGaugeRailway wideGaugeRailway)
        {
            _wideGaugeRailway = wideGaugeRailway ?? throw new ArgumentNullException(nameof(wideGaugeRailway));
        }

        /// <summary>
        /// Конвертує ширину ширококолійної колії у європейський формат
        /// </summary>
        public int GetEuropeanGaugeWidth()
        {
            return GaugeConverter.ConvertWideGaugeToEuropean(_wideGaugeRailway.GetWideGaugeWidth());
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
}
