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
  public class ProfissionalController : ControllerBase
  {
    private readonly IProfissionalService _profissionalService;
    public ProfissionalController(IProfissionalService profissionalService)
    {
      _profissionalService = profissionalService;
    }

    [HttpGet]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<IEnumerable<ProfissionalModel>> Get()
    {
      return await _profissionalService.BuscaProfissionais();
    }

    [HttpGet("{id}", Name = "Profissional")]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<ProfissionalModel> Get(int id)
    {
      //return await _profissionalService.BuscaProfissional(id);
      ProfissionalModel profissionalModel = await _profissionalService.BuscaProfissional(id);
      return profissionalModel;
    }

    [HttpPost]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<bool> Post(ProfissionalModel profissional)
    {
      return await _profissionalService.CreateProfissional(profissional);
    }

    [HttpPut]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<bool> Put(ProfissionalModel profissional)
    {
      return await _profissionalService.UpdateProfissional(profissional);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<bool> Delete(int id)
    {
      return await _profissionalService.Delete(id);
    }

    [HttpGet("MedicoGetLookup")]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<ProfissionalLookup> MedicoGetLookup()
    {
      List<ProfissionalModel> listMedicos = await _profissionalService.BuscaMedicos();
      return new ProfissionalLookup()
      {
        Items = listMedicos
      };
    }

    [HttpPost("GetHorariosDisponiveisMedicos")]
    [Authorize(Roles = "medico,secretaria")]
    public List<HorarioModel> GetHorariosDisponiveisMedico([FromBody]Params param)
    {
      return _profissionalService.BuscaHorariosDisponiveisMedico(param.idMedico, param.data);
    }

    public class Params
    {
      public int idMedico { get; set; }
      public DateTime data { get; set; }
    }
  }
}