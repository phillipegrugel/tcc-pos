using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Entities
{
  public class Paciente
  {
    [Key()]
    public int Id { get; set; }

    public bool PossuiConvenio { get; set; }

    public string NumeroCarteirinha { get; set; }

    public string NomeConvenio { get; set; }

    [ForeignKey("Pessoa")]
    public int PessoaId { get; set; }

    public virtual Pessoa Pessoa { get; set; }

    public bool Excluido { get; set; }
  }
}
