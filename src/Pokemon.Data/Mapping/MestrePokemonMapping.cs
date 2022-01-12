using Pokemon.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Data.Mapping
{
    public class MestrePokemonMapping : EntityTypeConfiguration<MestrePokemon>
    {
        public MestrePokemonMapping()
        {
            HasKey(c => c.Id);

            Property(c=>c.Id)
                .IsRequired();

            Property(c => c.Idade)
                .IsRequired();

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);

            Property(c => c.CpfLimpo)
                .IsRequired()
                .HasColumnName("Cpf")
                .HasMaxLength(11);

            Ignore(c => c.Cpf);

            ToTable("MestrePokemon");
        }
    }
}
