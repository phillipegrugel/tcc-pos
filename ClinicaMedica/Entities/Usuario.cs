using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Entities
{
  public class Usuario
  {
    [Key()]
    public int Id { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public string Role { get; set; }
    public bool Excluido { get; set; }
  }
}
