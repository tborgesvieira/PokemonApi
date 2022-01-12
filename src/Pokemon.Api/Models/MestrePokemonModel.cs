using System;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Api.Models
{
    public class MestrePokemonModel
    {
        public Guid Id { get; set; }

        [Required (ErrorMessage = "Nome deve ser informado")]
        [MaxLength(200, ErrorMessage = "Nome deve ter no máximo {0} caracteres")]
        public string Nome { get; set; }        
        public int Idade { get; set; }
        
        [Required(ErrorMessage = "CPF deve ser informado")]        
        public string Cpf { get; set; }
    }
}
