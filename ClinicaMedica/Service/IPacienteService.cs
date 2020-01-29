using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
  public interface IPacienteService
  {
    Task<bool> CreatePaciente(PacienteModel pacienteModel);
    Task<List<PacienteModel>> BuscaPacientes();
    Task<PacienteModel> BuscaPaciente(int id);
    Task<bool> UpdatePaciente(PacienteModel PacienteModel);
    Task<bool> Delete(int id);
  }
}
