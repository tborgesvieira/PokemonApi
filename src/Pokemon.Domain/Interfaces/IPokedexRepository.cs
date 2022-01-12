using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokemon.Domain.Interfaces
{
    public interface IPokedexRepository : IDisposable
    {
        Task<IEnumerable<Pokedex>> ObterPorMestrePokemon(Guid mestreId);
        Task<Pokedex> ObterPokedex(Guid mestreId, int pokemonId);
        Task<Pokedex> Adicionar(Pokedex pokedex);
    }
}
