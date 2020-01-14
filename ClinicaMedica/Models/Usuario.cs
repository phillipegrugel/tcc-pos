﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class Usuario
  {
    public int Id { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }

    public Profissional Profissional { get; set; }
  }
}
