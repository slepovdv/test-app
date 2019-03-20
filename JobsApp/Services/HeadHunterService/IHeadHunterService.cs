using JobsApp.Dto.HeadHunter;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobsApp.Services
{
    public interface IHeadHunterService
    {
        Task<Vacancy[]> GetVacancies();
    }
}
