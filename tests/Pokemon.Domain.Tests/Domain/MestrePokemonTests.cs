using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Domain.Tests.Domain
{
    public class MestrePokemonTests : IClassFixture<DomainFixture>
    {
        private readonly DomainFixture _domainFixture;

        public MestrePokemonTests(DomainFixture domainFixture)
        {
            _domainFixture = domainFixture;
        }

        [Fact]
        public void Dominio_MestrePokemon_NomeInvalido()
        {
            //Arrange
            var mestre = new MestrePokemon(_domainFixture.ObterCpfFaker(), "", 10);

            //Act
            Action action = () => mestre.Validar();
            var exception = Record.Exception(() => action());

            //Assert            
            Assert.Equal("Nome não pode ficar em branco", exception.Message);
        }

        [Fact]
        public void Dominio_MestrePokemon_TamanhoNomeInvalido()
        {
            //Arrange
            var textoFaker = new Faker().Random.String2(201);

            var mestre = new MestrePokemon(_domainFixture.ObterCpfFaker(), textoFaker, 10);

            //Act
            Action action = () => mestre.Validar();
            var exception = Record.Exception(() => action());

            //Assert            
            Assert.Equal("Tamanho do nome tem que ser de no máximo 200", exception.Message);
        }

        [Fact]
        public void Dominio_MestrePokemon_IdadeInvalido()
        {
            //Arrange            
            var mestre = new MestrePokemon(_domainFixture.ObterCpfFaker(), "Teste", 0);

            //Act
            Action action = () => mestre.Validar();
            var exception = Record.Exception(() => action());

            //Assert            
            Assert.Equal("Idade deve ser maior que 0", exception.Message);
        }

        [Theory]        
        [InlineData("")]
        [InlineData("00000000000")]
        [InlineData(null)]
        public void Dominio_MestrePokemon_CPFInvalido(string cpf)
        {
            //Arrange                        
            //Act
            Action action = () => new MestrePokemon(cpf, "Teste", 0);
            var exception = Record.Exception(() => action());

            //Assert            
            Assert.Equal("CPF inválido", exception.Message);
        }
    }
}
