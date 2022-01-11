using Pokemon.Domain.ValueObjects;
using System;

namespace Pokemon.Domain
{
    public class MestrePokemon
    {
        public Guid Id { get; private set; }
        public Cpf Cpf { get; private set; }
        public string Nome { get; private set; }

        public MestrePokemon(string cpfNumero, string nome)
        {            
            var cpf = new Cpf(cpfNumero);

            Cpf = cpf;

            Nome = nome;
        }
    }
}
