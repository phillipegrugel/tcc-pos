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
    public class RemedioController : ControllerBase
    {
        private readonly IRemedioService _remedioService;
        public RemedioController(IRemedioService remedioService)
        {
            _remedioService = remedioService;
        }

        [HttpGet]
        [Authorize(Roles = "medico,secretaria")]
        public async Task<IEnumerable<RemedioModel>> Get()
        {
            return await _remedioService.BuscaRemedios();
        }

        [HttpGet("{id}", Name = "Remedios")]
        [Authorize(Roles = "medico,secretaria")]
        public async Task<RemedioModel> Get(int id)
        {
            return await _remedioService.BuscaRemedio(id);
        }

        [HttpPost]
        [Authorize(Roles = "medico,secretaria")]
        public async Task<dynamic> Post(RemedioModel remedio)
        {
            return await _remedioService.CreateRemedio(remedio);
        }

        [HttpPut]
        [Authorize(Roles = "medico,secretaria")]
        public async Task<dynamic> Put(RemedioModel remedio)
        {
            return await _remedioService.UpdateRemedio(remedio);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "medico,secretaria")]
        public async Task<dynamic> Delete(int id)
        {
            return await _remedioService.Delete(id);
        }

        [HttpGet("GetLookup")]
        [Authorize(Roles = "medico,secretaria")]
        public async Task<RemedioLookup> GetLookup()
        {
            List<RemedioModel> listPacienteModel = await _remedioService.BuscaRemedios();
            return new RemedioLookup()
            {
                Items = listPacienteModel
            };
        }
    }
}