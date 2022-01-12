using AutoMapper;
using Pokemon.Api.Models;
using Pokemon.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pokemon.Api.Controllers
{
    public class MestrePokemonController : ApiController
    {
        private readonly IMestrePokemonService _mestrePokemonService;
        private readonly IMapper _mapper;

        public MestrePokemonController(IMestrePokemonService mestrePokemonService, IMapper mapper)
        {
            _mestrePokemonService = mestrePokemonService;

            _mapper = mapper;
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
