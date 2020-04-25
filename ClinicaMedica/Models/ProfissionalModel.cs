using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
    public class ProfissionalModel : PessoaModel
    {
        public int Id { get; set; }
        public string NumeroCarteiraTrabalho { get; set; }
        public string CRM { get; set; }
        
        //[Required(ErrorMessage = "Campo tipo obrigatório.")]
        public TipoProfissional Tipo { get; set; }

        //[Required(ErrorMessage = "Campo usuário obrigatório.")]
        public UsuarioModel Usuario { get; set; }
    }

    public enum TipoProfissional
    {
        Medico,
        Recepcionista
    }
}
