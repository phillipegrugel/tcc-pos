using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Entities
{
    public class PedidoExame
    {
        [Key()]
        public int Id { get; set; }

        public HistoricoClinico HistoricoClinico { get; set; }

        [ForeignKey("HistoricoClinico")]
        public int HistoricoClinicoId { get; set; }

        public Exame Exame { get; set; }

        [ForeignKey("Exame")]
        public int ExameId { get; set; }

        public string Resultado { get; set; }

        public bool EntreguePaciente { get; set; }
    }
}
