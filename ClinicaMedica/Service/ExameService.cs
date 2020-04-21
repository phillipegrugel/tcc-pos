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
    public class ExameService : IExameService
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

            pedidoExameModel.HistoricoClinico = _consultaService.GetHistoricoClinico(pedidoExame.HistoricoClinicoId, true);

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

        public async Task<bool> SalvaResultadoExame(PedidoExameModel pedidoExameModel)
        {
            try
            {
                PedidoExame pedidoExame = GetPedidoExameByModel(pedidoExameModel);
                _baseContext.PedidosExames.Add(pedidoExame);
                _baseContext.SaveChanges();
                return true;
            }
            catch
            {
                throw new Exception("Error");
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
    }
}
