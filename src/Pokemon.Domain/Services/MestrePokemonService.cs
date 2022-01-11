using Pokemon.Domain.Interfaces;
using Pokemon.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Domain.Services
{
    public class MestrePokemonService : IMestrePokemonService
    {
        private IMestrePokemonRepository _mestrePokemonRepository;

        public MestrePokemonService(IMestrePokemonRepository mestrePokemonRepository)
        {
            _mestrePokemonRepository = mestrePokemonRepository;
        }

        public void Dispose()
        {
            _mestrePokemonRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task<MestrePokemon> AdicionarMestrePokemon(string cpfNumero, string nome)
        {
            MestrePokemon mestrePokemon;

            mestrePokemon = await _mestrePokemonRepository.ObterPorCpf(new Cpf(cpfNumero));

            if(mestrePokemon == null)
            {
                mestrePokemon = new MestrePokemon(cpfNumero, nome);

                await _mestrePokemonRepository.Adicionar(mestrePokemon);
            }

            return mestrePokemon;
        }        
    }
}
