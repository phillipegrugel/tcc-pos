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
    Task<List<ConsultaModel>> BuscaConsultas(string login);
    Task<bool> UpdateConsulta(ConsultaModel consultaModel);
    Task<bool> Delete(int id);
    Task<bool> SalvarHistorico(ConsultaModel consulta);
    HistoricoClinicoModel GetHistoricoClinico(int id, bool addConsultaModel);
    Task<dynamic> GeraConsultaRapida(int idPaciente);
  }
}
