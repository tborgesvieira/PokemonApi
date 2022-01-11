using Pokemon.Domain.Helpers;
using System.Collections.Generic;

namespace Pokemon.Api.Models
{
    public class PokemonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseExperience { get; set; }
        public int Height { get; set; }
        public bool IsDefault { get; set; }
        public int Order { get; set; }
        public int Weight { get; set; }
        private string _sprite;
        public string Sprite
        {
            set
            {
                _sprite = value;
            }

            get
            {
                return Conversor.EncodeToBase64(_sprite);
            }
        }
        public List<string> Evolucoes { get; set; }
    }
}