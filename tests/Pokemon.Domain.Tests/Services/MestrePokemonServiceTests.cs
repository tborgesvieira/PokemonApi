using Moq;
using Pokemon.Domain.Interfaces;
using Pokemon.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Bogus;
using Bogus.Extensions.Brazil;
using Pokemon.Domain.ValueObjects;
using Pokemon.Domain.Helpers;

namespace Pokemon.Domain.Tests.Services
{
    public class MestrePokemonServiceTests
    {
        private readonly Mock<IMestrePokemonRepository> _mestrePokemonRepository;
        private readonly IMestrePokemonService _mestrePokemonService;

        public MestrePokemonServiceTests()
        {
            _mestrePokemonRepository = new Mock<IMestrePokemonRepository>();

            _mestrePokemonService = new MestrePokemonService(_mestrePokemonRepository.Object);
        }

        [Fact]
        public async void MestrePokemonService_Adicionar_Valido()
        {
            //Arrange
            var faker = new Faker("pt_BR");

            var cpfFaker = faker.Person.Cpf().ApenasNumeros();

            var nome = faker.Person.FullName;

            var idade = faker.Random.Int(10, 99);

            //Act
            var mestre = await _mestrePokemonService.AdicionarMestrePokemon(cpfFaker, nome, idade);

            //Assert
            Assert.NotNull(mestre);

            Assert.Equal(idade, mestre.Idade);

            Assert.Equal(nome, mestre.Nome);

            Assert.Equal(cpfFaker, mestre.Cpf.CpfLimpo);

            _mestrePokemonRepository.Verify(m => m.ObterPorCpf(It.Is<Cpf>(f=>f.CpfLimpo == cpfFaker)), Times.Once);
            
            _mestrePokemonRepository.Verify(m => m.Adicionar(mestre), Times.Once);            
        }

        [Fact]
        public async void MestrePokemonService_Adicionar_JaExistente()
        {
            //Arrange
            var mestreFaker = new Faker<MestrePokemon>("pt_BR")
                                .CustomInstantiator(f=> new MestrePokemon(f.Person.Cpf(), f.Person.FullName, f.Random.Int(10, 99)))
                                .RuleFor(c => c.Id, f => f.Random.Guid())                                
                                .Generate();

            _mestrePokemonRepository.Setup(m => m.ObterPorCpf(It.Is<Cpf>(f => f.CpfLimpo == mestreFaker.Cpf.CpfLimpo))).ReturnsAsync(mestreFaker);

            //Act
            var mestre = await _mestrePokemonService.AdicionarMestrePokemon(mestreFaker.Cpf.CpfLimpo, mestreFaker.Nome, mestreFaker.Idade);

            //Assert
            Assert.NotNull(mestre);

            Assert.Equal(mestreFaker.Idade, mestre.Idade);

            Assert.Equal(mestreFaker.Id, mestre.Id);

            Assert.Equal(mestreFaker.Nome, mestre.Nome);

            Assert.Equal(mestreFaker.Cpf.CpfLimpo, mestre.Cpf.CpfLimpo);

            _mestrePokemonRepository.Verify(m => m.ObterPorCpf(It.Is<Cpf>(f => f.CpfLimpo == mestreFaker.Cpf.CpfLimpo)), Times.Once);

            _mestrePokemonRepository.Verify(m => m.Adicionar(mestre), Times.Never);
        }
    }
}
