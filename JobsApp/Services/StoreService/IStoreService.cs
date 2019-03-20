using JobsApp.Models;
using System.Threading.Tasks;

namespace JobsApp.Services.StoreService
{
    public interface IStoreService
    {
        Task<VacancyModel[]> Get(string name, int salaryFrom);
    }
}
