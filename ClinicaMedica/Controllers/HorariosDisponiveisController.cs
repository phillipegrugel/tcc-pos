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
  public class HorariosDisponiveisController : ControllerBase
  {
    private readonly IProfissionalService _profissionalService;

    public HorariosDisponiveisController(IProfissionalService profissionalService)
    {
      _profissionalService = profissionalService;
    }

    [HttpPost]
    public List<HorarioModel> Post(Params param)
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