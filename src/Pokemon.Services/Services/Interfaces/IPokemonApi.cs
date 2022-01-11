using Pokemon.Services.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokemon.Services.Services.Interfaces
{
    public interface IPokemonApi : IDisposable
    {
        Task<IEnumerable<PokemonPerfil>> ObterPokemonsRandomicos(int quantidade);
        Task<PokemonEvolucaoChain> ObterEvolucoesPokemon(int idPokemon);
        Task<PokemonPerfil> ObterPokemonsPorNome(string nome);
    }
}
