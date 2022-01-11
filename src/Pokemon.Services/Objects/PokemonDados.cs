using System.Collections.Generic;

namespace Pokemon.Services.Objects
{
    public class PokemonDados
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public object Previous { get; set; }
        public IEnumerable<PokemonResult> Results { get; set; }
    }
}
