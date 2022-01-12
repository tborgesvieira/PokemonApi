using Pokemon.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Pokemon.Data.Mapping
{
    public class PokedexMapping : EntityTypeConfiguration<Pokedex>
    {
        public PokedexMapping()
        {
            HasKey(c=>c.Id);

            Property(c=>c.Id)
                .IsRequired();

            Property(c => c.MestrePokemonId)
                .IsRequired();            

            Property(c=>c.PokemonId)
                .IsRequired();

            Property(c => c.PokemonNome)
                .IsRequired()
                .HasMaxLength(200);

            ToTable("Pokedex");
        }
    }
}
