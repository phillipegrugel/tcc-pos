using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
  public interface IConsultaService
  {
    Task<bool> CreateConsulta(ConsultaModel consultaModel);
    Task<ConsultaModel> BuscaConsulta(int id);
    Task<List<ConsultaModel>> BuscaConsultas();
    Task<bool> UpdateConsulta(ConsultaModel consultaModel);
    Task<bool> Delete(int id);
  }
}
