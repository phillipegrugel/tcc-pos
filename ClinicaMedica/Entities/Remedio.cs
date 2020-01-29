using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Entities
{
  public class Remedio
  {
    [Key()]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NomeGenerico { get; set; }
    public string Fabricante { get; set; }
    public bool Excluido { get; set; }
  }
}
