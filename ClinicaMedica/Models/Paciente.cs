using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class Paciente : PessoaModel
  {
    public bool PossuiConvenio { get; set; }
    public string NumeroCarteirinha { get; set; }
    public string NomeConvenio { get; set; }
  }
}
