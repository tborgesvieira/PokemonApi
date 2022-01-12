using AutoMapper;
using Pokemon.Api.Models;
using Pokemon.Domain;
using Pokemon.Domain.Interfaces;
using Pokemon.Domain.ValueObjects;
using Pokemon.Services.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pokemon.Api.Controllers
{
    public class MestrePokemonController : ApiController
    {
        private readonly IMestrePokemonService _mestrePokemonService;
        private readonly IMestrePokemonRepository _mestrePokemonRepository;
        private readonly IPokedexRepository _pokedexRepository;
        private readonly IMapper _mapper;
        private readonly IPokemonApi _pokemonApi;

        public MestrePokemonController(
            IMestrePokemonService mestrePokemonService,
            IMestrePokemonRepository mestrePokemonRepository,
            IPokedexRepository pokedexRepository,
            IMapper mapper,
            IPokemonApi pokemonApi)
        {
            _mestrePokemonService = mestrePokemonService;

            _mestrePokemonRepository = mestrePokemonRepository;

            _pokedexRepository = pokedexRepository;

            _mapper = mapper;

            _pokemonApi = pokemonApi;
        }

        [HttpPost]
        [Route("api/capturar-pokemon/{cpf}/{pokemon}")]
        public async Task<IHttpActionResult> CapturarPokemon(string cpf, string pokemon)
        {
            var pokemonPerfil = await _pokemonApi.ObterPokemonsPorNome(pokemon);

            if (pokemon == null)
            {
                ModelState.AddModelError("pokemon", "Pokemon não localizado!");
                return BadRequest(ModelState);
            }

            Cpf cpfObject;

            try
            {
                cpfObject = new Cpf(cpf);
            }
            catch (Exception err)
            {
                ModelState.AddModelError("cpf", err.Message);
                return BadRequest(ModelState);
            }

            var mestre = await _mestrePokemonRepository.ObterPorCpf(cpfObject);

            if (mestre == null)
            {
                ModelState.AddModelError("mestre", "Mestre pokemon não localizado");
                return BadRequest(ModelState);
            }

            var pokedex = new Pokedex(mestre.Id, pokemonPerfil.Id, pokemonPerfil.Name);

            await _pokedexRepository.Adicionar(pokedex);

            return await ObterPokedex(cpf);
        }

        [HttpGet]
        [Route("api/pokedex/{cpf}")]
        public async Task<IHttpActionResult> ObterPokedex(string cpf)
        {
            Cpf cpfObject;

            try
            {
                cpfObject = new Cpf(cpf);
            }
            catch (Exception err)
            {
                ModelState.AddModelError("cpf", err.Message);
                return BadRequest(ModelState);
            }

            var mestre = await _mestrePokemonRepository.ObterPorCpf(cpfObject);

            if (mestre == null)
            {
                ModelState.AddModelError("mestre", "Mestre pokemon não localizado");
                return BadRequest(ModelState);
            }

            return Ok(await _pokedexRepository.ObterPorMestrePokemon(mestre.Id));
        }

        [HttpPut]
        [Route("api/adicionar-mestre-pokemon")]
        public async Task<IHttpActionResult> AdicionarMestrePokemon(MestrePokemonModel mestre)
        {
            if (mestre is null) return BadRequest("Informe os dados para cadatro");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var domain = await _mestrePokemonService.AdicionarMestrePokemon(mestre.Cpf, mestre.Nome, mestre.Idade);

                mestre = _mapper.Map<MestrePokemonModel>(domain);

                return Ok(mestre);
            }
            catch (Exception err)
            {
                ModelState.AddModelError("erros", err.Message);

                return BadRequest(ModelState);
            }
        }
    }
}