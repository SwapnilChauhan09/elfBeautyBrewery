
using BusinessEntities;

namespace BusinessLogics.Interface
{
    public interface IBreweryManager
    {
        public Task<List<BreweryBusinessModel>> GetAllBreweriesList();
        public Task<List<BreweryBusinessModel>> AutocompleteBreweries(string search);
        public Task<List<BreweryBusinessModel>> SearchBreweries(BreweriesSearchRequestModel searchRequestModel);
    }
}
