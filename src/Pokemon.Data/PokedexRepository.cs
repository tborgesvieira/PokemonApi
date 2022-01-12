using Pokemon.Data.Context;
using Pokemon.Domain;
using Pokemon.Domain.Interfaces;
using Pokemon.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Data
{
    public class PokedexRepository : IPokedexRepository
    {
        private readonly PokemonContext _pokemonContext;

        public PokedexRepository(PokemonContext pokemonContext)
        {
            _pokemonContext = pokemonContext;
        }        

        public void Dispose()
        {
            _pokemonContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Pokedex> Adicionar(Pokedex pokedex)
        {
            _pokemonContext.Pokedexs.Add(pokedex);

            await _pokemonContext.SaveChangesAsync();

            return pokedex;
        }

        public Task<Pokedex> ObterPokedex(Guid mestreId, int pokemonId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Pokedex>> ObterPorMestrePokemon(Guid id)
        {
            return await _pokemonContext.Pokedexs.Where(c=>c.MestrePokemonId.Equals(id)).ToListAsync();
        }
    }
}
