using System.Linq;
using System.Threading.Tasks;
using JobsApp.Models;
using JobsApp.Services.DatabaseService;
using JobsApp.Services.HeadHunterService;

namespace JobsApp.Services.StoreService.Impl
{
    public class StoreService : IStoreService
    {
        private readonly IHeadHunterService _headHunterService;
        private readonly IDatabaseService _databaseService;
        private readonly IHeadHunterVacancyConverter _converter;

        public StoreService(IHeadHunterVacancyConverter vacancyConverter, IHeadHunterService headHunterService, IDatabaseService databaseService)
        {
            _converter = vacancyConverter;
            _headHunterService = headHunterService;
            _databaseService = databaseService;
        }

        public async Task<VacancyModel[]> Get(string name, int salaryFrom)
        {
            if (await _databaseService.HaveVacancies())
            {
                return await _databaseService.Get(name, salaryFrom);
            }
            var headHunterItems = await _headHunterService.GetVacancies();
            var databaseItems = headHunterItems.Select(_ => _converter.Convert(_)).ToArray();
            await _databaseService.Insert(databaseItems);

            return await _databaseService.Get(name, salaryFrom);
        }
    }
}
