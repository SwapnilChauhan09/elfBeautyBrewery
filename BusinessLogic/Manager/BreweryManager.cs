using AutoMapper;
using BusinessEntities;
using BusinessLogics.Interface;
using InfrastructureCommon;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;


namespace BusinessLogics.Manager
{
    public class BreweryManager : IBreweryManager
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;
        public BreweryManager(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Retrieves a list of all breweries.
        /// </summary>
        /// <returns>A list of BreweryBusinessModel.</returns>
        public async Task<List<BreweryBusinessModel>> GetAllBreweriesList()
        {

            if (_memoryCache.TryGetValue("BreweriesList", out List<BreweryBusinessModel> breweries))
            {
                return breweries;
            }

            var response = await CallApi(ConstantClass.breweryGetAll);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<List<BreweryExternalModel>>(json);
                List<BreweryBusinessModel> breweriesResponse = _mapper.Map<List<BreweryBusinessModel>>(resp);
                _memoryCache.Set("BreweriesList", breweriesResponse, TimeSpan.FromMinutes(10));  //10 minute of caching
                return breweriesResponse;
            }

            return null;
        }

        /// <summary>
        ///Search from Breweries list
        /// </summary>
        /// <returns>matched records and sorting result based on sort column</returns>
        public async Task<List<BreweryBusinessModel>> SearchBreweries(BreweriesSearchRequestModel searchRequestModel)
        {

            List<BreweryBusinessModel> breweriesList = await GetAllBreweriesList();
            List<BreweryBusinessModel> response = breweriesList;

            if (!string.IsNullOrEmpty(searchRequestModel.search))
            {
                var search = searchRequestModel.search.ToLower();
                response = await Search(search, response);


                //case if we want to use direct brewery search API

                //var response = await CallApi(ConstantClass.brewerySearch);
                //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    var json = await response.Content.ReadAsStringAsync();
                //    var resp = JsonConvert.DeserializeObject<List<BreweryExternalModel>>(json);
                //    List<BreweryBusinessModel> breweriesResponse = _mapper.Map<List<BreweryBusinessModel>>(resp);
                //    return breweriesResponse;
                //}
                //return null;

            }


            if (!string.IsNullOrEmpty(searchRequestModel.sortColumn))
            {
                searchRequestModel.sortOrder = string.IsNullOrEmpty(searchRequestModel.sortOrder) ? "ascending" : (searchRequestModel.sortOrder?.ToLower() == "desc" ? "descending" : "ascending");
                var sortExpression = $"{searchRequestModel.sortColumn} {searchRequestModel.sortOrder}";
                response = response.AsQueryable()
                .OrderBy(sortExpression)
                .ToList();
            }
            return response;

        }

        /// <summary>
        /// find the matched data from serach string, used for autocomplete drop down
        /// </summary>
        /// <param></param>
        /// <returns>Return Breweries List</returns>
        public async Task<List<BreweryBusinessModel>> AutocompleteBreweries(string search)
        {
            List<BreweryBusinessModel> breweriesList = await GetAllBreweriesList();
            List<BreweryBusinessModel> response = breweriesList;
            //throw new Exception();
            if (!string.IsNullOrEmpty(search))
            {
                response = await Search(search, breweriesList);
            }
            return response;
        }

        /// <summary>
        /// private common method for brewery third party api call
        /// </summary>
        private async Task<HttpResponseMessage> CallApi(string Url)
        {
            string BaseURL = ConstantClass.breweryBaseURL;
            HttpClient _httpClient = new HttpClient();
            var response = await _httpClient.GetAsync(BaseURL + Url);
            return response;
        }

        /// <summary>
        /// Search common method from brewery list
        /// </summary>
        private async Task<List<BreweryBusinessModel>> Search(string searchString, List<BreweryBusinessModel> breweries)
        {
            var response = breweries.Where(b =>
                    (!string.IsNullOrEmpty(b.BreweryName) && b.BreweryName.ToLower().Contains(searchString)) ||
                    (!string.IsNullOrEmpty(b.BreweryAddress) && b.BreweryAddress.ToLower().Contains(searchString)) ||
                    (!string.IsNullOrEmpty(b.City) && b.City.ToLower().Contains(searchString)) ||
                    (!string.IsNullOrEmpty(b.State) && b.State.ToLower().Contains(searchString)) ||
                    (!string.IsNullOrEmpty(b.PostalCode) && b.PostalCode.ToLower().Contains(searchString)) ||
                    (!string.IsNullOrEmpty(b.Country) && b.Country.ToLower().Contains(searchString)) ||
                    (!string.IsNullOrEmpty(b.City) && b.City.ToLower().Contains(searchString)) ||
                    (!string.IsNullOrEmpty(b.ContactNumber) && b.ContactNumber.ToLower().Contains(searchString))).ToList();
            return response;
        }
    }
    
}
