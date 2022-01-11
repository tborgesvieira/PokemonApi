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
    public class PokemonApiTests : IClassFixture<PokemonApiFixture>
    {
        private readonly Mock<IApiService> _apiService;
        private readonly IPokemonApi _pokemonApi;
        private readonly PokemonApiFixture _pokemonApiFixture;

        public PokemonApiTests(PokemonApiFixture pokemonApiFixture)
        {
            _apiService = new Mock<IApiService>();

            _pokemonApi = new PokemonApi(_apiService.Object);

            _pokemonApiFixture = pokemonApiFixture;
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
            

            _apiService.Setup(m => m.GetAsync("/pokemon/?limit=1"))
                    .Returns(() => Task.FromResult(
                        new System.Net.Http.HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Content = new StringContent(_pokemonApiFixture.ObterContentMock())
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
            _apiService.Setup(m => m.GetAsync("/api/v2/pokemon/"))
                    .Returns(() => Task.FromResult(
                        new System.Net.Http.HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Content = new StringContent(_pokemonApiFixture.ObterContentMock())
                        }));

            _apiService.Setup(m => m.GetAsync("/api/v2/pokemon/?limit=1"))
                    .Returns(() => Task.FromResult(
                        new System.Net.Http.HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Content = new StringContent(_pokemonApiFixture.ObterContentMock())
                        }));

            _apiService.Setup(m => m.GetAsync("/api/v2/pokemon/bulbasaur/"))
                    .Returns(() => Task.FromResult(
                        new System.Net.Http.HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.OK,
                            Content = new StringContent(_pokemonApiFixture.ObterPokemonMock())
                        }));

            //Act
            var pokemonsPerfil = await _pokemonApi.ObterPokemonsRandomicos(1);

            //Assert
            _apiService.Verify(m => m.GetAsync("/api/v2/pokemon/"), Times.Once);
            _apiService.Verify(m => m.GetAsync("/api/v2/pokemon/?limit=1"), Times.Once);
            _apiService.Verify(m => m.GetAsync("/api/v2/pokemon/bulbasaur/"), Times.Once);
        }
    }
}
