using JobsApp.Models;
using System.Threading.Tasks;

namespace JobsApp.Services.DatabaseService
{
    public interface IDatabaseService
    {
        Task<VacancyModel[]> Get(string name, int salaryFrom);
        Task<bool> HaveVacancies();
        Task Insert(VacancyModel[] vacancies);
    }
}
