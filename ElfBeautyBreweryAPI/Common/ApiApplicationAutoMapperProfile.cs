using AutoMapper;
using elfBeautyBrewery.Api.Application.Contracts.Brewery;
using elfBeautyBrewery.Api.Application.Domain.Brewery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elfBeautyBrewery.Api.Host.Common
{
    public class ApiApplicationAutoMapperProfile : Profile 
    {
        public ApiApplicationAutoMapperProfile()
        {
            CreateMap<BreweryDto, Brewery>();
            CreateMap<Brewery, BreweryDto>();
        }
    }
}
