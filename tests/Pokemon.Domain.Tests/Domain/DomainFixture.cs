using Bogus;
using Bogus.Extensions.Brazil;

namespace Pokemon.Domain.Tests.Domain
{
    public class DomainFixture
    {
        public string ObterCpfFaker()
        {
            return new Faker("pt_BR").Person.Cpf();
        }
    }
}
