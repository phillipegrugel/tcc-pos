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
  public class ProfissionalService : IProfissionalService
  {
    private readonly IProfissionalRepository _profissionalRepository;
    private readonly BaseContext _baseContext;

    public ProfissionalService(IProfissionalRepository profissionalRepository, BaseContext baseContext)
    {
      _profissionalRepository = profissionalRepository;
      _baseContext = baseContext;
    }

    public static string GerarHashMd5(string input)
    {
      MD5 md5Hash = MD5.Create();
      // Converter a String para array de bytes, que é como a biblioteca trabalha.
      byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

      // Cria-se um StringBuilder para recompôr a string.
      StringBuilder sBuilder = new StringBuilder();

      // Loop para formatar cada byte como uma String em hexadecimal
      for (int i = 0; i < data.Length; i++)
      {
        sBuilder.Append(data[i].ToString("x2"));
      }

      return sBuilder.ToString();
    }
    public async Task<bool> CreateProfissional(ProfissionalModel profissionalModel)
    {
      if (profissionalModel.Usuario.Senha != profissionalModel.Usuario.ConfirmarSenha)
        throw new Exception("Senhas diferentes");

      profissionalModel.Usuario.Senha = GerarHashMd5(profissionalModel.Usuario.Senha);

      Profissional profissional = ProfissionalByProfissionalModel(profissionalModel);
      
      try
      {
        await _profissionalRepository.AddAsync(profissional);
        await _baseContext.SaveChangesAsync();

        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }

    public async Task<bool> UpdateProfissional(ProfissionalModel profissionalModel)
    {
      if (profissionalModel.Usuario.Senha != profissionalModel.Usuario.ConfirmarSenha)
        throw new Exception("Senhas diferentes");

      Profissional profissional = ProfissionalByProfissionalModel(profissionalModel);

      try
      {
        await _profissionalRepository.UpdateAsync(profissional);
        await _baseContext.SaveChangesAsync();

        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }

    public async Task<bool> Delete(int id)
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
        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
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
      catch(Exception e)
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
          Senha = profissionalModel.Usuario.Senha
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
  }
}
