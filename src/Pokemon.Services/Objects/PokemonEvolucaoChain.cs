using System.Collections.Generic;

namespace Pokemon.Services.Objects
{
    public class PokemonEvolucaoChain
    {
        public int Id { get; set; }
        public Chain Chain { get; set; }
    }

    public class Chain
    {
        public List<PokemonEvolucao> Evolves_To { get; set; }
    }
}
