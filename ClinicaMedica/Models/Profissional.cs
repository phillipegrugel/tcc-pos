using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class Profissional : Pessoa
  {
    public string NumeroCarteiraTrabalho { get; set; }
    public string CRM { get; set; }
    public TipoProfissional Tipo { get; set; }
  }

  public enum TipoProfissional
  {
    Medico,
    Recepcionista
  }
}
