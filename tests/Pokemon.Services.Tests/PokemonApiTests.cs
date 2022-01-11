using Moq;
using Pokemon.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Services.Tests
{
    public class PokemonApiTests
    {
        private readonly Mock<IApiService> _apiService;
        private readonly IPokemonApi _pokemonApi;

        public PokemonApiTests()
        {
            _apiService = new Mock<IApiService>();

            _pokemonApi = new PokemonApi(_apiService.Object);
        }

        [Fact]
        public async void PokemonApi_ObterPokemonsRandomicos_FalhaComunicacaoBuscaDados()
        {
            //Arrange
            _apiService.Setup(m => m.GetAsync("/pokemon/?limit=1")).Throws(new Exception("a"));

            var message = "";

            //Act
            try
            {
                await _pokemonApi.ObterPokemonsRandomicos(10);
            }
            catch(Exception err)
            {
                message = err.Message;
            }

            //Assert            
            Assert.Equal("Falha de comunicação com o serviço", message);
        }

        [Fact]
        public async void PokemonApi_ObterPokemonsRandomicos_FalhaComunicacaoBuscaPerfil()
        {
            //Arrange
            var content = "{\"count\":1,\"next\":\"https://pokeapi.co/api/v2/pokemon/?offset=1&limit=1\",\"previous\":null,\"results\":[{\"name\":\"bulbasaur\",\"url\":\"https://pokeapi.co/api/v2/pokemon/1/\"}]}";

            _apiService.Setup(m => m.GetAsync("/pokemon/?limit=1"))
                    .Returns(() => Task.FromResult(
                        new System.Net.Http.HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Content = new StringContent(content)
                        }));

            _apiService.Setup(m => m.GetAsync("/pokemon/1/")).Throws(new Exception("a"));

            var message = "";

            //Act
            try
            {
                await _pokemonApi.ObterPokemonsRandomicos(1);
            }
            catch (Exception err)
            {
                message = err.Message;
            }

            //Assert            
            Assert.Equal("Falha de comunicação com o serviço", message);
        }


        [Fact]
        public async void PokemonApi_ObterPokemonsRandomicos_Ok()
        {
            //Arrange
            var content = "{\"count\":1,\"next\":\"https://pokeapi.co/api/v2/pokemon/?offset=1&limit=1\",\"previous\":null,\"results\":[{\"name\":\"bulbasaur\",\"url\":\"https://pokeapi.co/api/v2/pokemon/1/\"}]}";

            _apiService.Setup(m => m.GetAsync("/pokemon/?limit=1"))
                    .Returns(() => Task.FromResult(
                        new System.Net.Http.HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Content = new StringContent(content)
                        }));
            //Act
            var pokemonsPerfil = await _pokemonApi.ObterPokemonsRandomicos(1);

            //Assert
            _apiService.Verify(m => m.GetAsync("/pokemon/?limit=1"));
        }
    }
}
