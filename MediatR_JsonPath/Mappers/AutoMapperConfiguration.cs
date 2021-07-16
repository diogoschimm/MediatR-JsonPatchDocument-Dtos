using AutoMapper;
using MediatR_JsonPath.Domain.DataTransferObjects;
using MediatR_JsonPath.Domain.Entities;

namespace MediatR_JsonPath.Mappers
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.AddProfile(new DtoToDomainProfile());
                ps.AddProfile(new DomainToDtoProfile());
            });
        }
    }

    public class DtoToDomainProfile: Profile
    {
        public DtoToDomainProfile()
        {
            CreateMap<PessoaDto, Pessoa>();
        }
    }
    public class DomainToDtoProfile: Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Pessoa, PessoaDto>();
        }
    }
}
