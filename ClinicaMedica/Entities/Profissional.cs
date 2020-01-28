using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Entities
{
  public class Profissional
  {
    [Key()]
    public int Id { get; set; }

    public string NumeroCarteiraTrabalho { get; set; }

    public string CRM { get; set; }

    [ForeignKey("Pessoa")]
    public int PessoaId { get; set; }

    public virtual Pessoa Pessoa { get; set; }

    [ForeignKey("Funcionarios")]
    public int UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; }

    public TipoProfissional Tipo { get; set; }
    public bool Excluido { get; set; }
  }
}
