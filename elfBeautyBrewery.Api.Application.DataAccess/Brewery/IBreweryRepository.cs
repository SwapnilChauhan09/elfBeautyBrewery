namespace elfBeautyBrewery.Api.Application.DataAccess.Brewery
{
    public interface IBreweryRepository
    {
        public Task<HttpResponseMessage> GetBreweryDataAsync(string Url);
    }
}
