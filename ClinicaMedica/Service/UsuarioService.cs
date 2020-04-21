using ClinicaMedica.Context;
using ClinicaMedica.Entities;
using ClinicaMedica.Models;
using ClinicaMedica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly BaseContext _baseContext;

        public UsuarioService(IUsuarioRepository usuarioRepository, BaseContext baseContext)
        {
            _usuarioRepository = usuarioRepository;
            _baseContext = baseContext;
        }
        public async Task<UsuarioModel> Autenticacao(string login, string senha)
        {
            try
            {
                senha = UtilService.GerarHashMd5(senha);
                Usuario usuario = _baseContext.Usuarios.SingleOrDefault<Usuario>(p => p.Login == login && p.Senha == senha && p.Excluido == false);
                //Profissional profissional = _baseContext.Profissionais.SingleOrDefault<Profissional>(p => p.UsuarioId == usuario.Id && p.Excluido == false);
                return UsuarioModelByUsuario(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> IsMedico(string login)
        {
            try
            {
                Usuario usuario = _baseContext.Usuarios.SingleOrDefault(u => u.Login == login);
                return usuario.Role == "medico";
            }
            catch
            {
                return false;
            }
        }

        private UsuarioModel UsuarioModelByUsuario(Usuario usuario)
        {
            return new UsuarioModel()
            {
                Id = usuario.Id,
                Login = usuario.Login,
                Role = usuario.Role
            };
        }
    }
}
