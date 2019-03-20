using JobsApp.Services;
using JobsApp.Services.DatabaseService;
using JobsApp.Services.HeadHunterService;
using JobsApp.Services.StoreService.Impl;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace JobsApp.Tests
{
    public class StoreServiceTest
    {
        [Fact]
        public async Task StoreService_LoadFromHH_If_Empty()
        {
            // Arrange
            var hhConverter = Substitute.For<IHeadHunterVacancyConverter>();
            var hhService = Substitute.For<IHeadHunterService>();
            var dbService = Substitute.For<IDatabaseService>();
            var storeServiсe = new StoreService(hhConverter, hhService, dbService);

            dbService.HaveVacancies().Returns(false);

            // Act
            await storeServiсe.Get(null, 0);

            // Assert
            await hhService.Received(1).GetVacancies();
            await dbService.Received(1).Get(null, 0);
        }

        [Fact]
        public async Task StoreService_DoNotLoadFromHH_If_NotEmpty()
        {
            // Arrange
            var hhConverter = Substitute.For<IHeadHunterVacancyConverter>();
            var hhService = Substitute.For<IHeadHunterService>();
            var dbService = Substitute.For<IDatabaseService>();
            var storeServiсe = new StoreService(hhConverter, hhService, dbService);

            dbService.HaveVacancies().Returns(true);

            // Act
            await storeServiсe.Get(null, 0);

            // Assert
            await hhService.Received(0).GetVacancies();
            await dbService.Received(1).Get(null, 0);
        }
    }
}
