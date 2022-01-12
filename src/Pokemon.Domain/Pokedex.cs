using System;

namespace Pokemon.Domain
{
    public class Pokedex
    {
        public Guid Id { get; private set; }
        public Guid MestrePokemonId { get; private set; }        
        public int PokemonId { get; private set; }
        public string PokemonNome { get; private set; }

        protected Pokedex()
        {

        }

        public Pokedex(Guid mestreId, int pokemonId, string nomePokemon)
        {
            Id = Guid.NewGuid();

            MestrePokemonId = mestreId;

            PokemonId = pokemonId;

            PokemonNome = nomePokemon;
        }
    }
}
