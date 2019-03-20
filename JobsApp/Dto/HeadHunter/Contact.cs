using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsApp.Dto.HeadHunter
{
    /// <summary>
    /// Контактная информация
    /// </summary>
    public class Contacts
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Телефоны
        /// </summary>
        public List<Phone> Phones { get; set; }
    }
}
