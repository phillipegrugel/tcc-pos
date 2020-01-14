using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaMedica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaMedica.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProfissionalController : ControllerBase
  {
    [HttpGet]
    public IEnumerable<Profissional> Get()
    {
      List<Profissional> list = new List<Profissional>();
      list.Add(new Profissional() { Nome = "Phillipe", CPF = "090.593.666-38", DataNascimento = new DateTime(1989, 12, 25) });

      return list;
    }

    [HttpGet("{id}", Name = "Profissional")]
    public Profissional Get(int id)
    {
      return new Profissional() { Nome = "Phillipe", CPF = "090.593.666-38", DataNascimento = new DateTime(1989, 12, 25) };
    }

    [HttpPost]
    public void Post(Profissional profissional)
    {

    }
  }
}