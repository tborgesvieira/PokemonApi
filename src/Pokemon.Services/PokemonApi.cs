using Newtonsoft.Json;
using Pokemon.Services.Objects;
using Pokemon.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Services
{
    public class PokemonApi : IPokemonApi
    {
        private readonly IApiService _apiService;
        private readonly ObjectCache _cache;
        public PokemonApi(IApiService apiService)
        {
            _apiService = apiService;
            _cache = MemoryCache.Default;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<PokemonEvolucaoChain> ObterEvolucoesPokemon(int idPokemon)
        {
            PokemonEvolucaoChain pokemonEvolucao;

            try
            {
                var data = await _apiService.GetAsync($"/api/v2/evolution-chain/{idPokemon}");

                if (data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var strData = await data.Content.ReadAsStringAsync();

                    pokemonEvolucao = JsonConvert.DeserializeObject<PokemonEvolucaoChain>(strData);
                }
                else
                {
                    pokemonEvolucao = null;
                }
            }
            catch
            {
                throw new Exception("Falha de comunicação com o serviço");
            }

            return pokemonEvolucao;
        }

        public async Task<PokemonPerfil> ObterPokemonsPorNome(string nome)
        {
            PokemonPerfil perfil = null;

            try
            {
                var data = await _apiService.GetAsync($"/api/v2/pokemon/{nome}/");

                if (data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var dataJson = await data.Content.ReadAsStringAsync();

                    perfil = JsonConvert.DeserializeObject<PokemonPerfil>(dataJson);
                }

                return perfil;
            }
            catch
            {
                throw new Exception("Falha de comunicação com o serviço");
            }
        }

        public async Task<IEnumerable<PokemonPerfil>> ObterPokemonsRandomicos(int quantidade)
        {
            PokemonDados pokemonDados;

            try
            {
                pokemonDados = (PokemonDados)_cache["PokemonDados"];

                if (pokemonDados == null)
                {
                    var data = await _apiService.GetAsync("/api/v2/pokemon/");

                    var strData = await data.Content.ReadAsStringAsync();

                    pokemonDados = JsonConvert.DeserializeObject<PokemonDados>(strData);

                    data = await _apiService.GetAsync($"/api/v2/pokemon/?limit={pokemonDados.Count}");

                    strData = await data.Content.ReadAsStringAsync();

                    pokemonDados = JsonConvert.DeserializeObject<PokemonDados>(strData);

                    var policy = new CacheItemPolicy()
                    {
                        AbsoluteExpiration = DateTimeOffset.MaxValue
                    };

                    _cache.Set("PokemonDados", pokemonDados, policy);
                }
            }
            catch
            {
                throw new Exception("Falha de comunicação com o serviço");
            }

            var pokemonsRandom = ObterRandomicos(quantidade, pokemonDados.Results.ToList());

            var pokemonsPerfil = await ObterPokemonsPerfil(pokemonsRandom);

            return pokemonsPerfil;
        }

        private async Task<IEnumerable<PokemonPerfil>> ObterPokemonsPerfil(List<PokemonResult> pokemonResults)
        {
            var pokemonPerfis = new List<PokemonPerfil>(pokemonResults.Count);

            foreach (var pokemon in pokemonResults)
            {
                try
                {
                    var perfil = await ObterPokemonsPorNome(pokemon.Name);

                    pokemonPerfis.Add(perfil);
                }
                catch
                {
                    throw new Exception("Falha de comunicação com o serviço");
                }
            }

            return pokemonPerfis;
        }

        private List<PokemonResult> ObterRandomicos(int quantidade, List<PokemonResult> results)
        {
            var pokemonsResult = new List<PokemonResult>(quantidade);

            var random = new Random();

            while (!pokemonsResult.Count.Equals(quantidade))
            {
                var id = random.Next(1, results.Count());

                var pkResult = results[id-1];

                if (!pokemonsResult.Contains(pkResult))
                {
                    pokemonsResult.Add(pkResult);
                }
            }

            return pokemonsResult;
        }
    }
}
