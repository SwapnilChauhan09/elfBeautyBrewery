using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elfBeautyBrewery.Api.Application.Domain.Brewery
{
    public class Brewery
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string BreweryType { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Address1 { get; set; }

        [MaxLength(200)]
        public string? Address2 { get; set; }

        [MaxLength(200)]
        public string? Address3 { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? StateProvince { get; set; }

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [Range(-180, 180)]
        public double? Longitude { get; set; }

        [Range(-90, 90)]
        public double? Latitude { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }

        [Url]
        [MaxLength(300)]
        public string? WebsiteUrl { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(200)]
        public string? Street { get; set; }
    }
}
