namespace Pokemon.Services.Objects
{
    public class PokemonPerfil
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Base_Experience { get; set; }
        public int Height { get; set; }
        public bool Is_Default { get; set; }
        public int Order { get; set; }
        public int Weight { get; set; }
        public Sprites Sprites { get; set; }
    }
}
