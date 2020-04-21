using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class ConsultaModel
  {
    public int Id { get; set; }
    public PacienteModel Paciente { get; set; }
    public ProfissionalModel Medico { get; set; }
    public DateTime Data { get; set; }
    public HorarioModel Horario { get; set; }
    public HistoricoClinicoModel HistoricoClinico { get; set; }
  }
}
