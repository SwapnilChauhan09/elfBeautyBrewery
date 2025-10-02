using elfBeautyBrewery.Api.Application;
using Microsoft.Extensions.Configuration;

namespace elfBeautyBrewery.Api.Application.DataAccess.Brewery
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly HttpClient _httpClient;

        public BreweryRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            var baseUrl = configuration.GetSection("Brewery")["BaseUrl"];

            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("BaseUrl configuration is missing or empty.", nameof(configuration));
            _httpClient.BaseAddress = new Uri(baseUrl, UriKind.Absolute);
        }

        public async Task<HttpResponseMessage> GetBreweryDataAsync(string relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(relativeUrl))
                throw new ArgumentException("relativeUrl cannot be null or empty.", nameof(relativeUrl));
            
            string absoluteUrl = new Uri(_httpClient.BaseAddress, relativeUrl).ToString();
            return await _httpClient.GetAsync(absoluteUrl);
        }
    }
}
