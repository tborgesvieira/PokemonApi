using Bogus;
using Bogus.Extensions.Brazil;
using Pokemon.Domain.Helpers;
using Pokemon.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Domain.Tests.ValueObjects
{
    public class CpfTests
    {

        [Theory]
        [InlineData("")]
        [InlineData("00000000000")]
        [InlineData("11111111111")]
        [InlineData("22222222222")]
        [InlineData("33333333333")]
        [InlineData("44444444444")]
        [InlineData("55555555555")]
        [InlineData("66666666666")]
        [InlineData("77777777777")]
        [InlineData("88888888888")]
        [InlineData("99999999999")]
        [InlineData("12345678911")]
        [InlineData("000.000.000-00")]
        [InlineData("111.111.111-11")]
        [InlineData("222.222.222-22")]
        [InlineData("333.333.333-33")]
        [InlineData("444.444.444-44")]
        [InlineData("555.555.555-55")]
        [InlineData("666.666.666-66")]
        [InlineData("777.777.777-77")]
        [InlineData("888.888.888-88")]
        [InlineData("999.999.999-99")]
        [InlineData("123.456.789-11")]
        public void CPF_Teste_Invalido(string cpf)
        {
            //Arrange            
            //Act
            Action act = () => new Cpf(cpf);

            //Assert
            var exception = Assert.Throws<Exception>(act);
            Assert.Equal("CPF inválido", exception.Message);
        }


        [Fact]
        public void CPF_Teste_Valido()
        {
            //Arrange
            var faker = new Faker("pt_BR");

            var cpfFake = faker.Person.Cpf();

            //Act
            var cpf = new Cpf(cpfFake);

            var numeros = cpfFake.ApenasNumeros();

            //Assert
            Assert.Equal(cpf.ObterCpfFormatado(), cpfFake);
            Assert.Equal(numeros, cpf.CpfLimpo);
        }
    }
}
