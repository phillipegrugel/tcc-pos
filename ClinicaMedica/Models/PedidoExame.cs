using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class PedidoExame
  {
    public int Id { get; set; }
    public Exame Exame { get; set; }
    public Paciente Paciente { get; set; }
  }
}
