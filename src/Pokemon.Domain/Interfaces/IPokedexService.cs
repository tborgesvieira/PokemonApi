using System;
using System.Threading.Tasks;

namespace Pokemon.Domain.Interfaces
{
    public interface IPokedexService :IDisposable
    {
        Task<Pokedex> Adicionar(Guid mestreId, int pokemonId, string pokemonNome);
    }
}
