using System.Collections.Generic;
using System.Threading.Tasks;
using JobsApp.Models;
using JobsApp.Services.StoreService;
using Microsoft.AspNetCore.Mvc;

namespace JobsApp.Controllers
{
    [Route("api/vacancies")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly IStoreService _store;

        public VacanciesController(IStoreService storeService)
        {
            _store = storeService;
        }

        [HttpGet]
        public async Task<IEnumerable<VacancyModel>> Get(string name = null, int salaryFrom = 0)
        {
            return await _store.Get(name, salaryFrom);
        }
    }
}
