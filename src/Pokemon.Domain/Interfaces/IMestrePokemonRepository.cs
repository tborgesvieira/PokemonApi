using Pokemon.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Pokemon.Domain.Interfaces
{
    public interface IMestrePokemonRepository : IDisposable
    {
        Task<MestrePokemon> Adicionar(MestrePokemon mestrePokemon);
        Task<MestrePokemon> ObterPorCpf(Cpf cpf);
    }
}
