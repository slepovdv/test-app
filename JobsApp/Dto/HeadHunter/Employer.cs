namespace JobsApp.Dto.HeadHunter
{
    /// <summary>
    /// Короткое представление работодателя
    /// </summary>
    public class Employer
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Урл
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Признак того что прошел проверку на сайте
        /// </summary>
        public bool Trusted { get; set; }
    }
}
