using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
  public interface IPacienteService
  {
    Task<dynamic> CreatePaciente(PacienteModel pacienteModel);
    Task<List<PacienteModel>> BuscaPacientes();
    Task<PacienteModel> BuscaPaciente(int id);
    Task<dynamic> UpdatePaciente(PacienteModel PacienteModel);
    Task<dynamic> Delete(int id);
  }
}
