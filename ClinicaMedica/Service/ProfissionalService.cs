using ClinicaMedica.Context;
using ClinicaMedica.Entities;
using ClinicaMedica.Models;
using ClinicaMedica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
    public class ProfissionalService : ServiceBase, IProfissionalService
    {
        private readonly IProfissionalRepository _profissionalRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly BaseContext _baseContext;

        public ProfissionalService(IProfissionalRepository profissionalRepository, IPessoaRepository pessoaRepository, BaseContext baseContext)
        {
            _profissionalRepository = profissionalRepository;
            _pessoaRepository = pessoaRepository;
            _baseContext = baseContext;
        }

        private async Task<dynamic> Validacoes(ProfissionalModel profissionalModel)
        {
            if (profissionalModel.Usuario.Senha != profissionalModel.Usuario.ConfirmarSenha)
                return GeraRetornoError("Senhas diferentes");

            if (profissionalModel.Tipo == TipoProfissional.Medico && string.IsNullOrEmpty(profissionalModel.CRM))
            {
                return GeraRetornoNullError("CRM");
            }
            else if (profissionalModel.Tipo == TipoProfissional.Recepcionista && string.IsNullOrEmpty(profissionalModel.NumeroCarteiraTrabalho))
            {
                return GeraRetornoNullError("número de carteira de trabalho");
            }

            if (!IsCpf(profissionalModel.CPF))
            {
                return GeraRetornoError("CPF inválido.");
            }

            return null;
        }

        public async Task<dynamic> CreateProfissional(ProfissionalModel profissionalModel)
        {
            dynamic retornoValid = await Validacoes(profissionalModel);

            if (retornoValid != null)
            {
                return retornoValid;
            }

            profissionalModel.Usuario.Senha = UtilService.GerarHashMd5(profissionalModel.Usuario.Senha);

            Profissional profissional = ProfissionalByProfissionalModel(profissionalModel);

            Pessoa pessoaMesmoCPF = _baseContext.Pessoas.FirstOrDefault(p => p.Excluido == false && p.CPF == profissionalModel.CPF);

            if (pessoaMesmoCPF != null)
            {
                Profissional profissionalMesmoCPF = _baseContext.Profissionais.FirstOrDefault(p => p.Excluido == false && p.PessoaId == pessoaMesmoCPF.Id);

                if (profissionalMesmoCPF != null)
                    return GeraRetornoError($"O CPF {profissionalModel.CPF} já está cadastrado para o profissional {pessoaMesmoCPF.Nome}");
                else // Se não for profissional ainda é um paciente que vai virar profissional tbm
                    profissional.PessoaId = pessoaMesmoCPF.Id;
            }

            try
            {
                await _profissionalRepository.AddAsync(profissional);
                await _baseContext.SaveChangesAsync();

                return GeraRetornoSucess("Profissional cadastrado.");
            }
            catch (Exception e)
            {
                return GeraRetornoError(e.Message);
            }
        }

        public async Task<dynamic> UpdateProfissional(ProfissionalModel profissionalModel)
        {
            dynamic retornoValid = await Validacoes(profissionalModel);

            if (retornoValid != null)
            {
                return retornoValid;
            }

            Profissional profissional = ProfissionalByProfissionalModel(profissionalModel);

            try
            {
                await _profissionalRepository.UpdateAsync(profissional);
                await _pessoaRepository.UpdateAsync(profissional.Pessoa);
                await _baseContext.SaveChangesAsync();

                return GeraRetornoSucess("Profissional alterado.");
            }
            catch
            {
                return GeraRetornoError();
            }
        }

        public async Task<dynamic> Delete(int id)
        {
            try
            {
                Profissional profissional = _baseContext.Profissionais.SingleOrDefault<Profissional>(p => p.Id == id);
                profissional.Excluido = true;
                profissional.Pessoa = _baseContext.Pessoas.SingleOrDefault<Pessoa>(p => p.Id == profissional.PessoaId);
                profissional.Pessoa.Excluido = true;
                profissional.Usuario = _baseContext.Usuarios.SingleOrDefault<Usuario>(u => u.Id == profissional.UsuarioId);
                profissional.Usuario.Excluido = true;

                await _profissionalRepository.UpdateAsync(profissional);
                await _baseContext.SaveChangesAsync();
                return GeraRetornoSucess("Profissional excluído.");
            }
            catch
            {
                return GeraRetornoError();
            }
        }
        public async Task<List<ProfissionalModel>> BuscaProfissionais()
        {
            try
            {
                List<Profissional> profissionaisEntities = _baseContext.Profissionais.Where(p => p.Excluido == false).ToList();
                List<ProfissionalModel> listProfissionalModel = new List<ProfissionalModel>();
                foreach (Profissional profissional in profissionaisEntities)
                {
                    profissional.Pessoa = _baseContext.Pessoas.Find(profissional.PessoaId);
                    profissional.Usuario = _baseContext.Usuarios.Find(profissional.UsuarioId);
                    listProfissionalModel.Add(await ProfissionalModelByProfissional(profissional));
                }

                return listProfissionalModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<ProfissionalModel>();
            }
        }

        public async Task<List<ProfissionalModel>> BuscaMedicos()
        {
            try
            {
                List<Profissional> profissionaisEntities = _baseContext.Profissionais.Where(p => p.Excluido == false && p.Tipo == TipoProfissional.Medico).ToList();
                List<ProfissionalModel> listProfissionalModel = new List<ProfissionalModel>();
                foreach (Profissional profissional in profissionaisEntities)
                {
                    profissional.Pessoa = _baseContext.Pessoas.Find(profissional.PessoaId);
                    profissional.Usuario = _baseContext.Usuarios.Find(profissional.UsuarioId);
                    listProfissionalModel.Add(await ProfissionalModelByProfissional(profissional));
                }

                return listProfissionalModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<ProfissionalModel>();
            }
        }

        public async Task<ProfissionalModel> BuscaProfissional(int id)
        {
            try
            {
                Profissional profissional = _baseContext.Profissionais.SingleOrDefault<Profissional>(p => p.Id == id && p.Excluido == false);
                profissional.Pessoa = _baseContext.Pessoas.SingleOrDefault<Pessoa>(p => p.Id == profissional.PessoaId && p.Excluido == false);
                profissional.Usuario = _baseContext.Usuarios.SingleOrDefault<Usuario>(u => u.Id == profissional.UsuarioId && profissional.Excluido == false);
                return await ProfissionalModelByProfissional(profissional);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ProfissionalModel();
            }
        }

        private async Task<ProfissionalModel> ProfissionalModelByProfissional(Profissional profissional)
        {
            return new ProfissionalModel()
            {
                CPF = profissional.Pessoa.CPF,
                CRM = profissional.CRM,
                DataNascimento = profissional.Pessoa.DataNascimento,
                Email = profissional.Pessoa.Email,
                Id = profissional.Id,
                IdPessoa = profissional.PessoaId,
                Nome = profissional.Pessoa.Nome,
                NumeroCarteiraTrabalho = profissional.NumeroCarteiraTrabalho,
                Telefone = profissional.Pessoa.Telefone,
                Tipo = profissional.Tipo,
                Usuario = new UsuarioModel()
                {
                    Id = profissional.Usuario.Id,
                    Login = profissional.Usuario.Login,
                    Senha = profissional.Usuario.Senha
                }
            };
        }
        private Profissional ProfissionalByProfissionalModel(ProfissionalModel profissionalModel)
        {
            return new Profissional()
            {
                CRM = profissionalModel.CRM,
                Id = profissionalModel.Id,
                NumeroCarteiraTrabalho = profissionalModel.NumeroCarteiraTrabalho,
                PessoaId = profissionalModel.IdPessoa,
                UsuarioId = profissionalModel.Usuario.Id,
                Tipo = profissionalModel.Tipo,
                Pessoa = new Pessoa()
                {
                    Id = profissionalModel.IdPessoa,
                    CPF = profissionalModel.CPF,
                    DataNascimento = profissionalModel.DataNascimento,
                    Email = profissionalModel.Email,
                    Nome = profissionalModel.Nome,
                    Telefone = profissionalModel.Telefone
                },
                Usuario = new Usuario()
                {
                    Id = profissionalModel.Usuario.Id,
                    Login = profissionalModel.Email,
                    Senha = profissionalModel.Usuario.Senha,
                    Role = profissionalModel.Tipo == TipoProfissional.Medico ? "medico" : "secretaria"
                }
            };
        }

        public List<HorarioModel> BuscaHorariosDisponiveisMedico(int idMedico, DateTime data)
        {
            Profissional profissional = _baseContext.Profissionais.SingleOrDefault<Profissional>(p => p.Id == idMedico && p.Excluido == false);
            List<HorarioModel> horariosDisponiveis = new List<HorarioModel>();
            foreach (HorarioModel horario in TodosHorarios())
            {
                Consulta consulta = _baseContext.Consultas.SingleOrDefault<Consulta>(c => c.Excluido == false && c.ProfissionalId == idMedico && c.Data == data && c.Horario == horario.Value);
                if (consulta == null)
                    horariosDisponiveis.Add(horario);
            }

            return horariosDisponiveis;
        }

        public List<HorarioModel> TodosHorarios()
        {
            List<HorarioModel> listaHorarios = new List<HorarioModel>();
            listaHorarios.Add(new HorarioModel() { Value = 0, Label = "08:00" });
            listaHorarios.Add(new HorarioModel() { Value = 1, Label = "09:00" });
            listaHorarios.Add(new HorarioModel() { Value = 2, Label = "10:00" });
            listaHorarios.Add(new HorarioModel() { Value = 3, Label = "11:00" });
            listaHorarios.Add(new HorarioModel() { Value = 4, Label = "12:00" });
            listaHorarios.Add(new HorarioModel() { Value = 5, Label = "13:00" });
            listaHorarios.Add(new HorarioModel() { Value = 6, Label = "14:00" });
            listaHorarios.Add(new HorarioModel() { Value = 7, Label = "15:00" });
            listaHorarios.Add(new HorarioModel() { Value = 8, Label = "16:00" });
            listaHorarios.Add(new HorarioModel() { Value = 9, Label = "17:00" });

            return listaHorarios;
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
