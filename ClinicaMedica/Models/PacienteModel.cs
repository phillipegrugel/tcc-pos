using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
    public class PacienteModel : PessoaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo possui convênio é obrigatório.")]
        public bool PossuiConvenio { get; set; }
        public string NumeroCarteirinha { get; set; }
        public string NomeConvenio { get; set; }
    }
}
