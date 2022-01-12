using Pokemon.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Pokemon.Domain
{
    public class MestrePokemon
    {
        public const int MAX_LENGTH_NOME = 200;

        public Guid Id { get; private set; }
        public Cpf Cpf { get; private set; }
        public string CpfLimpo
        {
            get
            {
               return Cpf.CpfLimpo;
            }
            set
            {
                Cpf = new Cpf(value);
            }
        }
        public string Nome { get; private set; }
        public int Idade { get; private set; }

        protected MestrePokemon(){}

        public MestrePokemon(string cpfNumero, string nome, int idade)
        {            
            Id = Guid.NewGuid();

            var cpf = new Cpf(cpfNumero);

            Cpf = cpf;

            Nome = nome;

            Idade = idade;
        }

        public void Validar()
        {
            ValidarNome();

            ValidarCPFPreenchido();            

            ValidarIdade();
        }

        private void ValidarIdade()
        {
            if (Idade == 0)
            {
                throw new Exception("Idade deve ser maior que 0");
            }
        }

        private void ValidarCPFPreenchido()
        {
            if (Cpf is null)
            {
                throw new Exception("CPF deve ser informado");
            }
        }

        private void ValidarNome()
        {
            if (Nome.Length > MAX_LENGTH_NOME)
            {
                throw new Exception($"Tamanho do nome tem que ser de no máximo {MAX_LENGTH_NOME}");
            }

            if (string.IsNullOrWhiteSpace(Nome))
            {
                throw new Exception("Nome não pode ficar em branco");
            }
        }
    }
}
