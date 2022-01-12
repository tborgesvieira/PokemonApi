using Pokemon.Data.Context;
using Pokemon.Domain;
using Pokemon.Domain.Interfaces;
using Pokemon.Domain.ValueObjects;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Data
{
    public class MestrePokemonRepository : IMestrePokemonRepository
    {
        private readonly PokemonContext _pokemonContext;

        public MestrePokemonRepository(PokemonContext pokemonContext)
        {
            _pokemonContext = pokemonContext;
        }

        public void Dispose()
        {
            _pokemonContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<MestrePokemon> Adicionar(MestrePokemon mestrePokemon)
        {
            _pokemonContext.MestresPokemons.Add(mestrePokemon);

            await _pokemonContext.SaveChangesAsync();

            return mestrePokemon;
        }        

        public async Task<MestrePokemon> ObterPorCpf(Cpf cpf)
        {
            return await _pokemonContext.MestresPokemons.FirstOrDefaultAsync(c => c.CpfLimpo.Equals(cpf.CpfLimpo));
        }
    }
}
