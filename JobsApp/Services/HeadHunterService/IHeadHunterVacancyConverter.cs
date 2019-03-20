using JobsApp.Dto.HeadHunter;
using JobsApp.Models;

namespace JobsApp.Services.HeadHunterService
{
    public interface IHeadHunterVacancyConverter
    {
        VacancyModel Convert(Vacancy vacancy);
    }
}
