using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
    public class PedidoExameModel
    {
        public int Id { get; set; }
        public ExameModel Exame { get; set; }
        public HistoricoClinicoModel HistoricoClinico { get; set; }
        public string Resultado { get; set; }
        public bool EntreguePaciente { get; set; }
    }
}
