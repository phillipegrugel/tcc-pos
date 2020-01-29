using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class HistoricoClinico
  {
    public int Id { get; set; }
    public Consulta Consulta { get; set; }
    public PedidoExame PedidoExame { get; set; }
    public List<RemedioModel> Remedios { get; set; }
    public string Observacao { get; set; }
  }
}
