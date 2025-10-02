using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elfBeautyBrewery.Api.Application.Contracts.Brewery
{
    public class BreweriesSearchRequest
    {
        [MaxLength(100)]
        public string Search { get; set; } = string.Empty;

        [MaxLength(50)]
        public string SortColumn { get; set; } = string.Empty;

        public string SortOrder { get; set; } = string.Empty;
    }
}
