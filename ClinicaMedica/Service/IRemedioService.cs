using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
  public interface IRemedioService
  {
    Task<bool> CreateRemedio(RemedioModel remedioModel);
    Task<List<RemedioModel>> BuscaRemedios();
    Task<RemedioModel> BuscaRemedio(int id);
    Task<bool> UpdateRemedio(RemedioModel remedioModel);
    Task<bool> Delete(int id);
  }
}
