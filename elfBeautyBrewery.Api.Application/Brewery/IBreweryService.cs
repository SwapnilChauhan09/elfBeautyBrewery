using elfBeautyBrewery.Api.Application.Contracts.Brewery;
using elfBeautyBrewery.Api.Application.Domain.Brewery;

namespace elfBeautyBrewery.Api.Application
{
    public interface IBreweryService
    {
        public Task<List<Brewery>> GetAllBreweriesList();
        public Task<List<Brewery>> SearchBreweries(BreweriesSearchRequest searchRequest);
        public Task<Brewery> GetBrewery(Guid id);
    }
}
