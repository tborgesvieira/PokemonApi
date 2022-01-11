using AutoMapper;
using Pokemon.Api.Models;
using Pokemon.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pokemon.Api.Controllers
{
    public class PokemonController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IPokemonApi _pokemonApi;

        public PokemonController(IPokemonApi pokemonApi, IMapper mapper)
        {
            _pokemonApi = pokemonApi;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/obter-dez-pokemons-aleatorios")]
        public async Task<IHttpActionResult> ObterPokemons()
        {
            try
            {
                var pokemons = await _pokemonApi.ObterPokemonsRandomicos(10);

                var viewModels = _mapper.Map<IEnumerable<PokemonModel>>(pokemons);

                foreach (var model in viewModels)
                {
                    await BuscarEvolucoes(model);
                }

                return Ok(viewModels);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet]
        [Route("api/obter-pokemon/{nome}")]
        public async Task<IHttpActionResult> ObterPokemons(string nome)
        {
            try
            {
                var pokemons = await _pokemonApi.ObterPokemonsPorNome(nome);

                var viewModel = _mapper.Map<PokemonModel>(pokemons);

                if(viewModel == null)
                {
                    return NotFound();
                }

                await BuscarEvolucoes(viewModel);

                return Ok(viewModel);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        private async Task BuscarEvolucoes(PokemonModel model)
        {
            if (model == null) return;

            var evolution = await _pokemonApi.ObterEvolucoesPokemon(model.Id);

            model.Evolucoes = new List<string>();

            if (evolution != null)
            {                
                evolution.Chain.Evolves_To.ToList().ForEach(e => model.Evolucoes.Add(e.Species.Name));
            }

        }
    }
}
