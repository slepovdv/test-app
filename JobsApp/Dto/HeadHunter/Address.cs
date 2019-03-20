namespace JobsApp.Dto.HeadHunter
{
    /// <summary>
    /// Адрес вакансии
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Номер дома
        /// </summary>
        public string Building { get; set; }
        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Географическая широта
        /// </summary>
        public double? Lat { get; set; }
        /// <summary>
        /// Географическая долгота
        /// </summary>
        public double? Lng { get; set; }
        /// <summary>
        /// Текстовое описание
        /// </summary>
        public string Raw { get; set; }
    }
}
