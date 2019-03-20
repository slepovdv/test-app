using JobsApp.Dto.HeadHunter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobsApp.Services.Impl
{
    public class HeadHunterService : IHeadHunterService
    {
        private const string HeadHunterUserAgent = "HH-User-Agent";
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private readonly HeadHunterServiceConfig _seviceConfig;

        public HeadHunterService(ILogger<HeadHunterService> logger, HeadHunterServiceConfig headHunterServiceConfig)
        {
            _logger = logger;
            _seviceConfig = headHunterServiceConfig;
            _httpClient = new HttpClient();
            _jsonSerializerSettings = new JsonSerializerSettings();
            _jsonSerializerSettings.ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
        }

        private async Task<string> GetRequestResponse()
        {
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Get;
            request.Headers.Add("User-Agent", HeadHunterUserAgent); // без него hh.ru реджектит
            request.RequestUri = new System.Uri($"{_seviceConfig.ApiUrl}/vacancies?area={_seviceConfig.AreaId}&per_page={_seviceConfig.PerPage}");
            var data = await _httpClient.SendAsync(request);

            if (!data.IsSuccessStatusCode)
            {
                _logger.LogError($"Не удалось получить ответ от hh.ru. Код ошибки: {data.StatusCode} Сообщение: {data.ReasonPhrase}");
                return string.Empty;
            }

            return await data.Content.ReadAsStringAsync();
        }

        private Vacancy[] ConvertFromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new Vacancy[0];
            }
            try
            {
                var result = JsonConvert.DeserializeObject<HeadHunterVacancyList>(json, _jsonSerializerSettings);
                return result.Items.ToArray();
            }
            catch (JsonSerializationException e)
            {
                _logger.LogError($"Не удалось распарсить ответ от hh.ru: {e.Message}");
                return new Vacancy[0];
            }
        }

        public async Task<Vacancy[]> GetVacancies()
        {
            var json = await GetRequestResponse();
            return ConvertFromJson(json);
        }
    }
}
