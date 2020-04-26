using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
  public interface IRemedioService
  {
    Task<dynamic> CreateRemedio(RemedioModel remedioModel);
    Task<List<RemedioModel>> BuscaRemedios();
    Task<RemedioModel> BuscaRemedio(int id);
    Task<dynamic> UpdateRemedio(RemedioModel remedioModel);
    Task<dynamic> Delete(int id);
  }
}
