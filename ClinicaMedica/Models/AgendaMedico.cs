using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class AgendaMedico
  {
    public int Id { get; set; }
    public Profissional Medico { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public List<DateTime> HorariosLivres { get; set; }
    public List<DateTime> HorariosOcupados { get; set; }
  }
}
