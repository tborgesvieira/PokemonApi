using AutoMapper;
using Pokemon.Api.Models;
using Pokemon.Domain;

namespace Pokemon.Api.Automapper
{
    public class MestrePokemonMapperConfig : Profile
    {
        public MestrePokemonMapperConfig()
        {
            CreateMap<MestrePokemon, MestrePokemonModel>()
                .ForMember(m => m.Cpf, f => f.MapFrom(mp => mp.Cpf.CpfLimpo));

            CreateMap<MestrePokemonModel, MestrePokemon>()
                .ConstructUsing(mp => new MestrePokemon(mp.Cpf, mp.Nome, mp.Idade));
        }
    }
}
