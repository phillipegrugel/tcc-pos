using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Entities
{
    public class RemedioReceita
    {
        [Key()]
        public int Id { get; set; }

        public Receita Receita { get; set; }

        [ForeignKey("Receita")]
        public int ReceitaId { get; set; }

        public Remedio Remedio { get; set; }

        [ForeignKey("Remedio")]
        public int RemedioId { get; set; }

        public string Observacoes { get; set; }
    }
}
