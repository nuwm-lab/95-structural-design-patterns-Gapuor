namespace LabWork.Adapters
{
    /// <summary>
    /// Централізований сервіс для конвертації ширини залізничних колій
    /// Дотримується принципу Single Responsibility
    /// </summary>
    public static class GaugeConverter
    {
        /// <summary>
        /// Різниця між ширококолійною та європейською колією (в мм)
        /// Європейська колія: 1435 мм (UIC стандарт)
        /// Ширококолійна колія: 1676 мм (Russian standard)
        /// Різниця: 1676 - 1435 = 241 мм
        /// </summary>
        public const int GaugeConversionDifference = 241;

        /// <summary>
        /// Конвертує ширину європейської колії в ширококолійну
        /// </summary>
        /// <param name="europeanGauge">Ширина європейської колії в мм</param>
        /// <returns>Конвертована ширина для ширококолійної системи</returns>
        public static int ConvertEuropeanToWideGauge(int europeanGauge)
        {
            return europeanGauge + GaugeConversionDifference;
        }

        /// <summary>
        /// Конвертує ширину ширококолійної колії в європейську
        /// </summary>
        /// <param name="wideGauge">Ширина ширококолійної колії в мм</param>
        /// <returns>Конвертована ширина для європейської системи</returns>
        public static int ConvertWideGaugeToEuropean(int wideGauge)
        {
            int result = wideGauge - GaugeConversionDifference;
            return result > 0 ? result : 0;
        }
    }
}
