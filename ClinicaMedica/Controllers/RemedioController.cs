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
  public class RemedioController : ControllerBase
  {
    private readonly IRemedioService _remedioService;
    public RemedioController(IRemedioService remedioService)
    {
      _remedioService = remedioService;
    }

    [HttpGet]
    public async Task<IEnumerable<RemedioModel>> Get()
    {
      return await _remedioService.BuscaRemedios();
    }

    [HttpGet("{id}", Name = "Remedios")]
    public async Task<RemedioModel> Get(int id)
    {
      return await _remedioService.BuscaRemedio(id);
    }

    [HttpPost]
    public async Task<bool> Post(RemedioModel remedio)
    {
      return await _remedioService.CreateRemedio(remedio);
    }

    [HttpPut]
    public async Task<bool> Put(RemedioModel remedio)
    {
      return await _remedioService.UpdateRemedio(remedio);
    }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
      return await _remedioService.Delete(id);
    }
  }
}