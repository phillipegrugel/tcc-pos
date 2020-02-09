using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaMedica.Models;
using ClinicaMedica.Service;
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
    public async Task<IEnumerable<ConsultaModel>> Get()
    {
      return await _consultaService.BuscaConsultas();
    }

    [HttpGet("{id}", Name = "Consultas")]
    public async Task<ConsultaModel> Get(int id)
    {
      return await _consultaService.BuscaConsulta(id);
    }

    [HttpPost]
    public async Task<bool> Post(ConsultaModel consulta)
    {
      return await _consultaService.CreateConsulta(consulta);
    }

    [HttpPut]
    public async Task<bool> Put(ConsultaModel consulta)
    {
      return await _consultaService.UpdateConsulta(consulta);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
      return await _consultaService.Delete(id);
    }
  }
}