using ClinicaMedica.Context;
using ClinicaMedica.Entities;
using ClinicaMedica.Models;
using ClinicaMedica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
    public class ExameService : ServiceBase, IExameService
    {
        private readonly IExameRepository _exameRepository;
        private readonly BaseContext _baseContext;
        private readonly IConsultaService _consultaService;
        public ExameService(IExameRepository exameRepository, BaseContext baseContext, IConsultaService consultaService)
        {
            _baseContext = baseContext;
            _exameRepository = exameRepository;
            _consultaService = consultaService;
        }
        public async Task<List<ExameModel>> BuscaExames()
        {
            try
            {
                List<Exame> exames = _baseContext.Exames.ToList();
                List<ExameModel> listExamesModel = new List<ExameModel>();

                foreach (Exame exame in exames)
                {
                    listExamesModel.Add(await GetExameModelByExame(exame));
                }

                return listExamesModel;
            }
            catch
            {
                return null;
            }
        }

        private async Task<ExameModel> GetExameModelByExame(Exame exame)
        {
            return new ExameModel
            {
                Id = exame.Id,
                Nome = exame.Nome
            };
        }

        public async Task<ExameModel> Get(int id)
        {
            try
            {
                Exame exame = this._baseContext.Exames.FirstOrDefault(e => e.Id == id);
                return await GetExameModelByExame(exame);
            }
            catch
            {
                return null;
            }
        }

        public List<PedidoExameModel> BuscaExamesPendentes()
        {
            try
            {
                List<PedidoExame> exames = _baseContext.PedidosExames.Where(e => e.Resultado == null || e.Resultado == "").ToList();
                List<PedidoExameModel> exameModels = new List<PedidoExameModel>();
                foreach (PedidoExame pedidoExame in exames)
                {
                    exameModels.Add(PedidoExameModelByPedidoExame(pedidoExame));
                }
                return exameModels;
            }
            catch
            {
                throw new Exception("Error");
            }
        }

        private PedidoExameModel PedidoExameModelByPedidoExame(PedidoExame pedidoExame)
        {
            Exame exame = _baseContext.Exames.FirstOrDefault(e => e.Id == pedidoExame.ExameId);
            ExameModel exameModel = ExameModelByExame(exame);

            PedidoExameModel pedidoExameModel = new PedidoExameModel
            {
                Id = pedidoExame.Id,
                EntreguePaciente = pedidoExame.EntreguePaciente,
                Resultado = pedidoExame.Resultado,
                Exame = exameModel
            };

            HistoricoClinico historicoClinico = _baseContext.HistoricosClinicos.FirstOrDefault(h => h.Id == pedidoExame.HistoricoClinicoId);
            Consulta consulta = _baseContext.Consultas.FirstOrDefault(c => c.Id == historicoClinico.ConsultaId);
            Paciente paciente = _baseContext.Pacientes.FirstOrDefault(p => p.Id == consulta.PacienteId);
            Pessoa pessoa = _baseContext.Pessoas.FirstOrDefault(p => p.Id == paciente.PessoaId);

            pedidoExameModel.HistoricoClinico = new HistoricoClinicoModel
            {
                Id = historicoClinico.Id,
                Consulta = new ConsultaModel
                {
                    Paciente = new PacienteModel
                    {
                        Nome = pessoa.Nome,
                        CPF  = "just",
                        DataNascimento = DateTime.Now,
                        Email = "just",
                        Telefone = "just"
                    }
                }
            };

            return pedidoExameModel;
        }

        private ExameModel ExameModelByExame(Exame exame)
        {
            return new ExameModel
            {
                Id = exame.Id,
                Nome = exame.Nome
            };
        }

        public async Task<dynamic> SalvaResultadoExame(PedidoExameModel pedidoExameModel)
        {
            try
            {
                PedidoExame pedidoExame = GetPedidoExameByModel(pedidoExameModel);
                _baseContext.PedidosExames.Update(pedidoExame);
                _baseContext.SaveChanges();
                return await GeraRetornoSucess("Resultado de exame cadastrado.");
            }
            catch
            {
                return await GeraRetornoError();
            }
        }

        private PedidoExame GetPedidoExameByModel(PedidoExameModel pedidoExameModel)
        {
            return new PedidoExame
            {
                Id = pedidoExameModel.Id,
                EntreguePaciente = pedidoExameModel.EntreguePaciente,
                ExameId = pedidoExameModel.Exame.Id,
                HistoricoClinicoId = pedidoExameModel.HistoricoClinico.Id,
                Resultado = pedidoExameModel.Resultado
            };
        }

        public async Task<PedidoExameModel> BuscaExamePendente(int id)
        {
            try
            {
                PedidoExame pedidoExame = _baseContext.PedidosExames.FirstOrDefault(p => p.Id == id);
                PedidoExameModel pedidoExameModel = PedidoExameModelByPedidoExame(pedidoExame);
                return pedidoExameModel;
            }
            catch
            {
                throw new Exception("Error");
            }
        }
    }
}
