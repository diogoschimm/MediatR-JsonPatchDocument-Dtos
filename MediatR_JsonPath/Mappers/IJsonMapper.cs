﻿using Microsoft.AspNetCore.JsonPatch;

namespace MediatR_JsonPath.Mappers
{
    public interface IJsonMapper
    {
        TDomain ToDomain<TDto, TDomain>(JsonPatchDocument<TDto> data, TDomain domain) where TDto : class where TDomain : class;
        TDomain ToDomain<TDto, TDomain>(TDto dto) where TDto : class where TDomain : class;

        TDto ToDto<TDomain, TDto>(TDomain domain) where TDto : class where TDomain : class;
    }
}
