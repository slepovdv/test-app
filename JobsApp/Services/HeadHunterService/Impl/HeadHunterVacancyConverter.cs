using JobsApp.Dto.HeadHunter;
using JobsApp.Models;
using System;
using System.Linq;

namespace JobsApp.Services.HeadHunterService.Impl
{
    public class HeadHunterVacancyConverter : IHeadHunterVacancyConverter
    {
        public VacancyModel Convert(Vacancy vacancy)
        {
            var model = new VacancyModel();
            model.Id = Guid.NewGuid();
            model.Name = vacancy.Name;
            model.Organization = vacancy.Employer?.Name;
            model.Phone = GetContactPhone(vacancy);
            model.ContactFullName = vacancy.Contacts?.Name;
            model.Address = GetAddress(vacancy);
            model.SalaryFrom = GetSalaryFrom(vacancy);
            model.SalaryTo = GetSalaryTo(vacancy);
            model.Type = GetType(vacancy);
            model.Description = vacancy.Description;
            model.Requierments = vacancy.Snippet?.Requirement;
            model.Responsibility = vacancy.Snippet?.Responsibility;

            return model;
        }

        private string GetType(Vacancy vacancy)
        {
            if (vacancy.Employment == null)
            {
                return "Полная";
            }
            return vacancy.Employment.Name;
        }

        private int GetSalaryFrom(Vacancy vacancy)
        {
            if (vacancy.Salary == null)
            {
                return 0;
            }
            if (!vacancy.Salary.From.HasValue)
            {
                return 0;
            }
            return vacancy.Salary.From.Value;
        }

        private int GetSalaryTo(Vacancy vacancy)
        {
            if (vacancy.Salary == null)
            {
                return 0;
            }
            if (!vacancy.Salary.To.HasValue)
            {
                return 0;
            }
            return vacancy.Salary.To.Value;
        }

        private string GetAddress(Vacancy vacancy)
        {
            if (vacancy.Address == null)
            {
                return string.Empty;
            }
            return string.Join(" ", vacancy.Address.City, vacancy.Address.Street, vacancy.Address.Building);
        }

        private string GetContactPhone(Vacancy vacancy)
        {
            var phones = vacancy.Contacts?.Phones;
            if (phones != null)
            {
                var phone = phones.FirstOrDefault();
                if (phone != null)
                {
                    return $"+{phone.Country}{phone.City}{phone.Number}";
                }
            }
            return string.Empty;
        }
    }
}
