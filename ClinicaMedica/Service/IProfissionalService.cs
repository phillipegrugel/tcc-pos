using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
  public interface IProfissionalService
  {
    Task<dynamic> CreateProfissional(ProfissionalModel profissionalModel);
    Task<List<ProfissionalModel>> BuscaProfissionais();
    Task<ProfissionalModel> BuscaProfissional(int id);
    Task<dynamic> UpdateProfissional(ProfissionalModel profissionalModel);
    Task<dynamic> Delete(int id);
    Task<List<ProfissionalModel>> BuscaMedicos();
    List<HorarioModel> BuscaHorariosDisponiveisMedico(int idMedico, DateTime data);
  }
}
