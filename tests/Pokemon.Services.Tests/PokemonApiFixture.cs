using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Services.Tests
{
    public class PokemonApiFixture
    {
        public string ObterContentMock()
        {
            var content = "{\"count\":1,\"next\":\"https://pokeapi.co/api/v2/pokemon/?offset=1&limit=1\",\"previous\":null,\"results\":[{\"name\":\"bulbasaur\",\"url\":\"https://pokeapi.co/api/v2/pokemon/1/\"}]}";

            return content;
        }

        public string ObterPokemonMock()
        {
            var bubasaur = $"{Directory.GetCurrentDirectory()}\\Mock\\bubasaur.json";

            return File.ReadAllText(bubasaur);
        }
    }
}
