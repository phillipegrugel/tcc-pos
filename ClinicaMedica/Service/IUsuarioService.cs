using ClinicaMedica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
    public interface IUsuarioService
    {
        Task<UsuarioModel> Autenticacao(string login, string senha);
        Task<bool> IsMedico(string login);
    }
}