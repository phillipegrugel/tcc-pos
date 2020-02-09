using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Entities
{
  public class Consulta
  {
    [Key()]
    public int Id { get; set; }

    public Paciente Paciente { get; set; }

    [ForeignKey("Paciente")]
    public int PacienteId { get; set; }

    public Profissional Medico { get; set; }

    [ForeignKey("Profissional")]
    public int ProfissionalId { get; set; }

    public DateTime Data { get; set; }

    public int Horario { get; set; }

    public bool Excluido { get; set; }
  }
}
