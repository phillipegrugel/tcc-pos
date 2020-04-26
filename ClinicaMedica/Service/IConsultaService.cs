using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
    public interface IConsultaService
    {
        Task<dynamic> CreateConsulta(ConsultaModel consultaModel);
        Task<ConsultaModel> BuscaConsulta(int id);
        Task<List<ConsultaModel>> BuscaConsultas(string login);
        Task<dynamic> UpdateConsulta(ConsultaModel consultaModel);
        Task<dynamic> Delete(int id);
        Task<dynamic> SalvarHistorico(ConsultaModel consulta);
        HistoricoClinicoModel GetHistoricoClinico(int id, bool addConsultaModel);
        Task<dynamic> GeraConsultaRapida(int idPaciente);
    }
}
