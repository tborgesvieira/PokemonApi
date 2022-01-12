using AutoMapper;
using Pokemon.Api.Models;
using Pokemon.Services.Objects;

namespace Pokemon.Api.Automapper
{
    public class PokemonMapperConfig : Profile
    {
        public PokemonMapperConfig()
        {
            CreateMap<PokemonPerfil, PokemonModel>()                
                .ForMember(p => p.Sprite, f => f.MapFrom(pk => pk.Sprites.Front_Default))
                .ForMember(p => p.BaseExperience, f => f.MapFrom(pk => pk.Base_Experience))
                .ForMember(p => p.IsDefault, f => f.MapFrom(pk => pk.Is_Default));                        
        }
    }
}