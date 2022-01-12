using Pokemon.Data.Mapping;
using Pokemon.Domain;
using SQLite.CodeFirst;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Pokemon.Data.Context
{
    public class PokemonContext : DbContext
    {
        public PokemonContext() : base("PokemonConnection") 
        {
            
        }
       
        public DbSet<MestrePokemon> MestresPokemons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MestrePokemonMapping());
        }
    }
}
