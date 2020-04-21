using ClinicaMedica.Context;
using ClinicaMedica.Entities;
using ClinicaMedica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Repositories
{
    public class HistoricoClinicoRepository : Repository<HistoricoClinico>, IHistoricoClinicoRepository
    {
        public HistoricoClinicoRepository(BaseContext baseContext): base(baseContext)
        {

        }
    }
}
