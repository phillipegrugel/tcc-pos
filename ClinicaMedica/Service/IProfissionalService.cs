using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
  public interface IProfissionalService
  {
    Task<bool> CreateProfissional(ProfissionalModel profissionalModel);
    Task<List<ProfissionalModel>> BuscaProfissionais();
    Task<ProfissionalModel> BuscaProfissional(int id);
    Task<bool> UpdateProfissional(ProfissionalModel profissionalModel);
    Task<bool> Delete(int id);
  }
}
