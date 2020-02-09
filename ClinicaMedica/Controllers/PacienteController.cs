using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaMedica.Models;
using ClinicaMedica.Models.Lookups;
using ClinicaMedica.Service;
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
    public async Task<IEnumerable<PacienteModel>> Get()
    {
      return await _pacienteService.BuscaPacientes();
    }

    [HttpGet("{id}", Name = "Pacientes")]
    public async Task<PacienteModel> Get(int id)
    {
      return await _pacienteService.BuscaPaciente(id);
    }

    [HttpPost]
    public async Task<bool> Post(PacienteModel paciente)
    {
      return await _pacienteService.CreatePaciente(paciente);
    }

    [HttpPut]
    public async Task<bool> Put(PacienteModel paciente)
    {
      return await _pacienteService.UpdatePaciente(paciente);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
      return await _pacienteService.Delete(id);
    }

    [HttpGet("GetLookup")]
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