namespace JobsApp.Dto.HeadHunter
{
    /// <summary>
    /// Зарплата
    /// </summary>
    public class Salary
    {
        /// <summary>
        /// Нижняя граница
        /// </summary>
        public int? From { get; set; }
        /// <summary>
        /// Верхняя граница
        /// </summary>
        public int? To { get; set; }
        /// <summary>
        /// Валюта
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Признак того что оклад указан до вычета налогов
        /// </summary>
        public bool Gross { get; set; }
    }
}
