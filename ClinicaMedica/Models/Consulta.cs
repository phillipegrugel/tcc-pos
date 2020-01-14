using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class Consulta
  {
    public int Id { get; set; }
    public Paciente Paciente { get; set; }
    public Profissional Medico { get; set; }
    public DateTime DiaHora { get; set; }
  }
}
