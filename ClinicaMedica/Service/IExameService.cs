﻿using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
    public interface IExameService
    {
        Task<List<ExameModel>> BuscaExames();
        Task<ExameModel> Get(int id);
        List<PedidoExameModel> BuscaExamesPendentes();
        Task<dynamic> SalvaResultadoExame(PedidoExameModel pedidoExame);
        Task<PedidoExameModel> BuscaExamePendente(int id);
    }
}
