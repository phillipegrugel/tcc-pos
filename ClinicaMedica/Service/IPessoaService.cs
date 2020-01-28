using ClinicaMedica.Entities;
using ClinicaMedica.Models;
using ClinicaMedica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
  public interface IPessoaService
  {
    Task<bool> CreatePessoa(PessoaModel pessoaModel);
    Task<bool> CreatePessoa();
  }
}
