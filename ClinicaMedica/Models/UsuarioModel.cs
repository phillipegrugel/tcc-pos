using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class UsuarioModel
  {
    public int Id { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
    public string ConfirmarSenha { get; set; }
    public ProfissionalModel Profissional { get; set; }
  }
}
