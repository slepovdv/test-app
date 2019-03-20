namespace JobsApp.Dto.HeadHunter
{
    /// <summary>
    /// Регион размещения вакансии
    /// </summary>
    public class Area
    {
        /// <summary>
        /// Идентификатор региона
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Название региона
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Url получения информации о регионе
        /// </summary>
        public string Url { get; set; }
    }
}
