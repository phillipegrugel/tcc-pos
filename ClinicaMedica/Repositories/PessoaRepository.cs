using ClinicaMedica.Context;
using ClinicaMedica.Entities;
using ClinicaMedica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Repositories
{
  public class PessoaRepository : Repository<Pessoa>, IPessoaRepository
  {
    public PessoaRepository(BaseContext context) : base(context)
    {
    }
  }
}
