using elfBeautyBrewery.Api.Application;
using elfBeautyBrewery.Api.Application.Contracts.Brewery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace elfBeautyBrewery.Api.Host.Controllers
{
    [ApiController]
    [Route("v{apiVersion:apiVersion}/api/[controller]")]
    [ApiVersion("1.0")]
    public class BreweryController : ControllerBase
    {
        private readonly IBreweryService _breweryService;
        private readonly ILogger<BreweryController> _logger;

        #region Constructor        
        public BreweryController(ILogger<BreweryController> logger, IBreweryService breweryService)
        {
            _logger = logger;
            _breweryService = breweryService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get all Breweries List
        /// </summary>
        /// <returns>Return Breweries List</returns>
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll called.");
            try
            {
                var response = await _breweryService.GetAllBreweriesList();
                _logger.LogInformation("GetAll succeeded.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetAll.");
                return StatusCode(500, "An error occurred while retrieving breweries.");
            }
        }

        /// <summary>
        /// Get Brewery by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return Brewery</returns>
        [HttpGet("Get")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation("Get called with Id: {Id}", id);
            try
            {
                var response = await _breweryService.GetBrewery(id);
                _logger.LogInformation("Get succeeded for Id: {Id}", id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Get for Id: {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the brewery.");
            }
        }

        /// <summary>
        /// Search from Breweries List along with sorted column and order
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns>Return Breweries List</returns>
        [HttpPost("Search")]
        [Authorize]
        public async Task<IActionResult> Search([FromBody] BreweriesSearchRequest searchRequest)
        {
            _logger.LogInformation("Search called with request: {@SearchRequest}", searchRequest);
            try
            {
                var breweries = await _breweryService.SearchBreweries(searchRequest);
                _logger.LogInformation("Search succeeded.");
                return Ok(breweries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Search.");
                return StatusCode(500, "An error occurred while searching breweries.");
            }
        }

        #endregion
    }
}
