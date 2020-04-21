using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Entities
{
    public class HistoricoClinico
    {
        [Key()]
        public int Id { get; set; }

        public Consulta Consulta { get; set; }

        [ForeignKey("Consulta")]
        public int ConsultaId { get; set; }

        public Receita Receita { get; set; }

        public List<PedidoExame> Exames { get; set; }

        public string Observacoes { get; set; }
    }
}
