using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Entities
{
    public class Receita
    {
        [Key()]
        public int Id { get; set; }

        public HistoricoClinico HistoricoClinico { get; set; }

        [ForeignKey("HistoricoClinico")]
        public int HistoricoClinicoId { get; set; }

        public List<RemedioReceita> Remedios { get; set; }

        public string Observacoes { get; set; }
    }
}
