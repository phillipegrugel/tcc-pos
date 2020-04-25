using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
    public class PessoaModel : BaseModel
    {
        public int IdPessoa { get; set; }

        [Required(ErrorMessage = "Campo nome obrigatório." )]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo CPF obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo data de nascimento obrigatório.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Campo e-mail obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo telefone obrigatório.")]
        public string Telefone { get; set; }
    }
}
