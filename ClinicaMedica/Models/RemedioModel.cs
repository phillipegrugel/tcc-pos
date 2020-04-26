using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
    public class RemedioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo nome genérico obrigatório.")]
        public string NomeGenerico { get; set; }

        [Required(ErrorMessage = "Campo fabricante obrigatório.")]
        public string Fabricante { get; set; }
    }
}
