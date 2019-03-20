using System;

namespace JobsApp.Models
{
    public class VacancyModel
    {
        /// <summary>
        /// Идентификатор вакансии
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Название вакансии
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Зарплата, от
        /// </summary>
        public int SalaryFrom { get; set; }

        /// <summary>
        /// Зарплата, да
        /// </summary>
        public int SalaryTo { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Контктное лицо
        /// </summary>
        public string ContactFullName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Тип вакансии
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Требования
        /// </summary>
        public string Requierments { get; set; }

        /// <summary>
        /// Обязанности
        /// </summary>
        public string Responsibility { get; set; }
    }
}
