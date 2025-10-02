using AutoMapper;
using elfBeautyBrewery.Api.Application.Contracts.Brewery;
using elfBeautyBrewery.Api.Application.DataAccess.Brewery;
using elfBeautyBrewery.Api.Application.Domain.Brewery;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace elfBeautyBrewery.Api.Application
{
    public class BreweryService : IBreweryService
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly IBreweryRepository _repo;
        private readonly ILogger _logger;

        private const string BreweriesListCacheKey = "Breweries";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

        public BreweryService(
            IMapper mapper,
            IMemoryCache cache,
            IBreweryRepository repo,
            ILogger<BreweryService> logger)
        {
            _mapper = mapper;
            _cache = cache;
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<Brewery>> GetAllBreweriesList()
            => await GetBreweriesListInternalAsync("breweries", BreweriesListCacheKey);

        public async Task<Brewery> GetBrewery(Guid id)
        {
            var cacheKey = $"{BreweriesListCacheKey}_{id}";
            if (_cache.TryGetValue(cacheKey, out Brewery cachedBrewery) && cachedBrewery != null)
                return cachedBrewery;

            try
            {
                var response = await _repo.GetBreweryDataAsync($"breweries/{id}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                var brewery = JsonConvert.DeserializeObject<Brewery>(json);
                if (brewery != null)
                {
                    brewery = _mapper.Map<Brewery>(brewery);
                    _cache.Set(cacheKey, brewery, CacheDuration);
                }
                return brewery;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving brewery with id {BreweryId}.", id);
                throw;
            }
        }

        private async Task<List<Brewery>> GetBreweriesListInternalAsync(string url, string cacheKey)
        {
            if (_cache.TryGetValue(cacheKey, out List<Brewery> breweries) && breweries != null)
                return breweries;

            try
            {
                var response = await _repo.GetBreweryDataAsync(url);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return new List<Brewery>();

                var json = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<List<Brewery>>(json);
                breweries = resp != null ? _mapper.Map<List<Brewery>>(resp) : new List<Brewery>();
                _cache.Set(cacheKey, breweries, CacheDuration);
                return breweries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving breweries.");
                throw;
            }
        }

        public async Task<List<Brewery>> SearchBreweries(BreweriesSearchRequest request)
        {
            var breweries = await GetAllBreweriesList(); 
            if (breweries == null || breweries.Count == 0)
                return new List<Brewery>();

            breweries = FilterBreweries(breweries, request.Search);
            breweries = SortBreweries(breweries, request.SortColumn, request.SortOrder);
            return breweries;
        }

        private static List<Brewery> FilterBreweries(List<Brewery> breweries, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return breweries;

            var s = search.ToLower();
            return breweries.Where(b =>
                (b.Name?.ToLower().Contains(s) ?? false) ||
                (b.State?.ToLower().Contains(s) ?? false) ||
                (b.PostalCode?.ToLower().Contains(s) ?? false) ||
                (b.Country?.ToLower().Contains(s) ?? false) ||
                (b.City?.ToLower().Contains(s) ?? false)
            ).ToList();
        }

        private static List<Brewery> SortBreweries(List<Brewery> breweries, string sortColumn, string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                return breweries;

            var prop = typeof(Brewery).GetProperty(
                sortColumn,
                System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance
            );
            if (prop == null)
                return breweries;

            var desc = string.Equals(sortOrder, "descending", StringComparison.OrdinalIgnoreCase);
            return desc
                ? breweries.OrderByDescending(x => prop.GetValue(x)).ToList()
                : breweries.OrderBy(x => prop.GetValue(x)).ToList();
        }
    }
}
