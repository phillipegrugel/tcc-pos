using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaMedica.Models;
using ClinicaMedica.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaMedica.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ConsultaController : ControllerBase
  {
    private readonly IConsultaService _consultaService;
    public ConsultaController(IConsultaService consultaService)
    {
      _consultaService = consultaService;
    }

    [HttpGet]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<IEnumerable<ConsultaModel>> Get()
    {
      return await _consultaService.BuscaConsultas(User.Identity.Name);
    }

    [HttpGet("{id}", Name = "Consultas")]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<ConsultaModel> Get(int id)
    {
      return await _consultaService.BuscaConsulta(id);
    }

    [HttpPost]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<bool> Post(ConsultaModel consulta)
    {
      return await _consultaService.CreateConsulta(consulta);
    }

    [HttpPut]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<bool> Put(ConsultaModel consulta)
    {
      return await _consultaService.UpdateConsulta(consulta);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<bool> Delete(int id)
    {
      return await _consultaService.Delete(id);
    }

        [HttpPost("SalvarHistorico")]
        [Authorize(Roles = "medico")]
        public async Task<bool> SalvarHistorico(ConsultaModel consulta)
        {
            return await _consultaService.SalvarHistorico(consulta);
        }

        [HttpPost("GeraConsultaRapida")]
        [Authorize]
        public async Task<dynamic> GeraConsultaRapida([FromBody]int idPaciente)
        {
            string mensagem = await _consultaService.GeraConsultaRapida(idPaciente);

            return new
            {
                message = mensagem
            };
        }
  }
}