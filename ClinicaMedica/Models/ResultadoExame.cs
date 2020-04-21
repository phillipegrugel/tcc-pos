using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class ResultadoExame
  {
    public int Id { get; set; }
    public PedidoExameModel PedidoExame { get; set; }
    public string Resultado { get; set; }
  }
}
