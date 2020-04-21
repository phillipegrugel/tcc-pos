using ClinicaMedica.Context;
using ClinicaMedica.Entities;
using ClinicaMedica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Repositories
{
    public class RemedioReceitaRepository : Repository<RemedioReceita>, IRemedioReceitaRepository
    {
        public RemedioReceitaRepository(BaseContext baseContext): base(baseContext)
        {

        }
    }
}
