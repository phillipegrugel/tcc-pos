using ClinicaMedica.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Context
{
  public class BaseContext : DbContext
  {
    public BaseContext(DbContextOptions<BaseContext> options) : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Profissional> Profissionais { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Remedio> Remedios { get; set; }
    public DbSet<Consulta> Consultas { get; set; }
  }
}
