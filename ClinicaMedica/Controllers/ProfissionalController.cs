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
  public class ProfissionalController : ControllerBase
  {
    private readonly IProfissionalService _profissionalService;
    public ProfissionalController(IProfissionalService profissionalService)
    {
      _profissionalService = profissionalService;
    }

    [HttpGet]
    public async Task<IEnumerable<ProfissionalModel>> Get()
    {
      return await _profissionalService.BuscaProfissionais();
    }

    [HttpGet("{id}", Name = "Profissional")]
    public async Task<ProfissionalModel> Get(int id)
    {
      //return await _profissionalService.BuscaProfissional(id);
      ProfissionalModel profissionalModel = await _profissionalService.BuscaProfissional(id);
      return profissionalModel;
    }

    [HttpPost]
    public async Task<bool> Post(ProfissionalModel profissional)
    {
      return await _profissionalService.CreateProfissional(profissional);
    }

    [HttpPut]
    public async Task<bool> Put(ProfissionalModel profissional)
    {
      return await _profissionalService.UpdateProfissional(profissional);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
      return await _profissionalService.Delete(id);
    }
  }
}