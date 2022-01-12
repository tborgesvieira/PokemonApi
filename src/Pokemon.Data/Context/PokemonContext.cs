using Pokemon.Data.Mapping;
using Pokemon.Domain;
using System.Data.Entity;

namespace Pokemon.Data.Context
{
    public class PokemonContext : DbContext
    {
        public PokemonContext() : base("PokemonConnection")
        {

        }

        public DbSet<MestrePokemon> MestresPokemons { get; set; }
        public DbSet<Pokedex> Pokedexs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #if DEBUG
            Database.Log = s => System.Diagnostics.Debug.Write(s);
            #endif

            modelBuilder.Configurations.Add(new MestrePokemonMapping());
            modelBuilder.Configurations.Add(new PokedexMapping());
        }
    }
}
