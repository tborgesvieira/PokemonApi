using System;
using System.Threading.Tasks;

namespace Pokemon.Domain.Interfaces
{
    public interface IMestrePokemonService : IDisposable
    {
        Task<MestrePokemon> AdicionarMestrePokemon(string cpfNumero, string nome);
    }
}
