using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaMedica.Models;
using ClinicaMedica.Models.Lookups;
using ClinicaMedica.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaMedica.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PacienteController : ControllerBase
  {
    private readonly IPacienteService _pacienteService;
    public PacienteController(IPacienteService pacienteService)
    {
      _pacienteService = pacienteService;
    }

    [HttpGet]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<IEnumerable<PacienteModel>> Get()
    {
      return await _pacienteService.BuscaPacientes();
    }

    [HttpGet("{id}", Name = "Pacientes")]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<PacienteModel> Get(int id)
    {
      return await _pacienteService.BuscaPaciente(id);
    }

    [HttpPost]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<dynamic> Post(PacienteModel paciente)
    {
      return await _pacienteService.CreatePaciente(paciente);
    }

    [HttpPut]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<dynamic> Put(PacienteModel paciente)
    {
      return await _pacienteService.UpdatePaciente(paciente);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<dynamic> Delete(int id)
    {
      return await _pacienteService.Delete(id);
    }

    [HttpGet("GetLookup")]
    [Authorize(Roles = "medico,secretaria")]
    public async Task<PacienteLookup> GetLookup()
    {
      List<PacienteModel> listPacienteModel = await _pacienteService.BuscaPacientes();
      return new PacienteLookup()
      {
        Items = listPacienteModel
      };
    }
  }
}