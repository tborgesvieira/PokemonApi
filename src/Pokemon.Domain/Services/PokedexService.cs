using Pokemon.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Domain.Services
{
    public class PokedexService : IPokedexService
    {
        private readonly IPokedexRepository _pokedexRepository;

        public PokedexService(IPokedexRepository pokedexRepository)
        {
            _pokedexRepository = pokedexRepository;
        }

        public void Dispose()
        {
            _pokedexRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<Pokedex> Adicionar(Guid mestreId, int pokemonId, string pokemonNome)
        {
            var pokedex = await _pokedexRepository.ObterPokedex(mestreId, pokemonId);

            if (pokedex != null)
            {
                return pokedex;
            }

            pokedex = new Pokedex(mestreId, pokemonId, pokemonNome);

            return await _pokedexRepository.Adicionar(pokedex);
        }
    }
}
