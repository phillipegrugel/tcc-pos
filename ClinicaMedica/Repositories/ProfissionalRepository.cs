using ClinicaMedica.Context;
using ClinicaMedica.Entities;
using ClinicaMedica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Repositories
{
  public class ProfissionalRepository : Repository<Profissional>, IProfissionalRepository
  {
    public ProfissionalRepository(BaseContext context) : base(context)
    {

    }
  }
}
