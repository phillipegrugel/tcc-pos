using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaMedica.Models;
using ClinicaMedica.Models.Lookups;
using ClinicaMedica.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaMedica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExameController : ControllerBase
    {
        private readonly IExameService _exameService;
        public ExameController(IExameService exameService)
        {
            _exameService = exameService;
        }

        [HttpGet("GetLookup")]
        [Authorize(Roles = "medico,secretaria")]
        public async Task<ExameLookup> GetLookup()
        {
            List<ExameModel> listExamesModel = await _exameService.BuscaExames();
            return new ExameLookup()
            {
                Items = listExamesModel
            };
        }

        [HttpGet("{id}", Name = "Exames")]
        [Authorize]
        public async Task<ExameModel> Get(int id)
        {
            return await _exameService.Get(id);
        }

        [HttpGet("GetExamesPendentes")]
        [Authorize]
        public List<PedidoExameModel> GetExamesPendentes()
        {
            return _exameService.BuscaExamesPendentes();
        }

        [HttpGet("GetExamePendente/{id}", Name = "GetExamePendente")]
        [Authorize]
        public async Task<PedidoExameModel> GetExamePendente(int id)
        {
            return await _exameService.BuscaExamePendente(id);
        }

        [HttpPost("SalvaResultadoExame")]
        [Authorize]
        public async Task<dynamic> SalvaResultadoExame(PedidoExameModel pedidoExame)
        {
            return await _exameService.SalvaResultadoExame(pedidoExame);
        }
    }
}