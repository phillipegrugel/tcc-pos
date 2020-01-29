using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class RemedioModel
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NomeGenerico { get; set; }
    public string Fabricante { get; set; }
  }
}
