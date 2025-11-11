using System;

namespace LabWork.Models
{
    /// <summary>
    /// Інтерфейс європейської залізничної колії
    /// </summary>
    public interface IEuropeanRailway
    {
        /// <summary>
        /// Отримує ширину європейської колії в мм
        /// </summary>
        int GetEuropeanGaugeWidth();

        /// <summary>
        /// Отримує стандарт європейської колії
        /// </summary>
        string GetEuropeanStandard();

        /// <summary>
        /// Отримує описання європейської колії
        /// </summary>
        string GetEuropeanDescription();
    }

    /// <summary>
    /// Інтерфейс ширококолійної залізничної колії
    /// </summary>
    public interface IWideGaugeRailway
    {
        /// <summary>
        /// Отримує ширину ширококолійної колії в мм
        /// </summary>
        int GetWideGaugeWidth();

        /// <summary>
        /// Отримує стандарт ширококолійної колії
        /// </summary>
        string GetWideGaugeStandard();

        /// <summary>
        /// Отримує описання ширококолійної колії
        /// </summary>
        string GetWideGaugeDescription();
    }

    /// <summary>
    /// Конкретна реалізація європейської залізничної колії
    /// </summary>
    public class EuropeanRailway : IEuropeanRailway
    {
        private const int StandardGaugeWidth = 1435; // в мм
        private readonly string _standard = "UIC (Union Internationale des Chemins de fer)";
        private readonly string _description = "Європейська залізниця стандартної колії";

        /// <summary>
        /// Отримує ширину європейської колії
        /// </summary>
        public int GetEuropeanGaugeWidth() => StandardGaugeWidth;

        /// <summary>
        /// Отримує стандарт європейської колії
        /// </summary>
        public string GetEuropeanStandard() => _standard;

        /// <summary>
        /// Отримує описання європейської колії
        /// </summary>
        public string GetEuropeanDescription() => _description;
    }

    /// <summary>
    /// Конкретна реалізація ширококолійної залізничної колії
    /// </summary>
    public class WideGaugeRailway : IWideGaugeRailway
    {
        private const int WideGaugeWidth = 1676; // в мм
        private readonly string _standard = "Russian Standard";
        private readonly string _description = "Ширококолійна залізниця для перевезення важких вантажів";

        /// <summary>
        /// Отримує ширину ширококолійної колії
        /// </summary>
        public int GetWideGaugeWidth() => WideGaugeWidth;

        /// <summary>
        /// Отримує стандарт ширококолійної колії
        /// </summary>
        public string GetWideGaugeStandard() => _standard;

        /// <summary>
        /// Отримує описання ширококолійної колії
        /// </summary>
        public string GetWideGaugeDescription() => _description;
    }
}
