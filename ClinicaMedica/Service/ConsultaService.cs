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
  public class ConsultaService : IConsultaService
  {
    private readonly IConsultaRepository _consultaRepository;
    private readonly BaseContext _baseContext;

    public ConsultaService(IConsultaRepository consultaRepository, BaseContext baseContext)
    {
      _consultaRepository = consultaRepository;
      _baseContext = baseContext;
    }
    public async Task<ConsultaModel> BuscaConsulta(int id)
    {
      try
      {
        Consulta consulta = _baseContext.Consultas.SingleOrDefault<Consulta>(p => p.Id == id && p.Excluido == false);
        consulta.Medico = _baseContext.Profissionais.SingleOrDefault<Profissional>(p => p.Id == consulta.ProfissionalId && p.Excluido == false);
        consulta.Medico.Pessoa = _baseContext.Pessoas.SingleOrDefault<Pessoa>(p => p.Id == consulta.Medico.PessoaId);
        consulta.Paciente = _baseContext.Pacientes.SingleOrDefault<Paciente>(p => p.Id == consulta.PacienteId && p.Excluido == false);
        consulta.Paciente.Pessoa = _baseContext.Pessoas.SingleOrDefault<Pessoa>(p => p.Id == consulta.Paciente.PessoaId);
        consulta.Medico.Usuario = _baseContext.Usuarios.SingleOrDefault<Usuario>(u => u.Id == consulta.Medico.UsuarioId);
        return await ConsultaModelByConsulta(consulta);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return new ConsultaModel();
      }
    }

    private async Task<ConsultaModel> ConsultaModelByConsulta(Consulta consulta)
    {
      string labelHorario = string.Empty;

      switch (consulta.Horario)
      {
        case 0:
          labelHorario = "08:00";
          break;
        case 1:
          labelHorario = "09:00";
          break;
        case 2:
          labelHorario = "10:00";
          break;
        case 3:
          labelHorario = "11:00";
          break;
        case 4:
          labelHorario = "12:00";
          break;
        case 5:
          labelHorario = "13:00";
          break;
        case 6:
          labelHorario = "14:00";
          break;
        case 7:
          labelHorario = "15:00";
          break;
        case 8:
          labelHorario = "16:00";
          break;
        case 9:
          labelHorario = "17:00";
          break;
      }

      return new ConsultaModel()
      {
        Id = consulta.Id,
        Data = consulta.Data,
        Horario = new HorarioModel()
        {
          Value = consulta.Horario,
          Label = labelHorario
        },
        Medico = new ProfissionalModel()
        {
          CPF = consulta.Medico.Pessoa.CPF,
          CRM = consulta.Medico.CRM,
          DataNascimento = consulta.Medico.Pessoa.DataNascimento,
          Email = consulta.Medico.Pessoa.Email,
          Id = consulta.Medico.Id,
          IdPessoa = consulta.Medico.PessoaId,
          Nome = consulta.Medico.Pessoa.Nome,
          NumeroCarteiraTrabalho = consulta.Medico.NumeroCarteiraTrabalho,
          Telefone = consulta.Medico.Pessoa.Telefone,
          Tipo = consulta.Medico.Tipo,
          Usuario = new UsuarioModel()
          {
            Id = consulta.Medico.Usuario.Id,
            Login = consulta.Medico.Usuario.Login,
            Senha = consulta.Medico.Usuario.Senha
          }
        },
        Paciente = new PacienteModel()
        {
          CPF = consulta.Paciente.Pessoa.CPF,
          DataNascimento = consulta.Paciente.Pessoa.DataNascimento,
          Email = consulta.Paciente.Pessoa.Email,
          Id = consulta.Paciente.Id,
          IdPessoa = consulta.Paciente.PessoaId,
          Nome = consulta.Paciente.Pessoa.Nome,
          Telefone = consulta.Paciente.Pessoa.Telefone,
          NomeConvenio = consulta.Paciente.NomeConvenio,
          NumeroCarteirinha = consulta.Paciente.NumeroCarteirinha,
          PossuiConvenio = consulta.Paciente.PossuiConvenio
        }
      };
    }

    public async Task<List<ConsultaModel>> BuscaConsultas()
    {
      try
      {
        List<Consulta> consultasEntities = _baseContext.Consultas.Where(p => p.Excluido == false).ToList();
        List<ConsultaModel> listConsultaModel = new List<ConsultaModel>();
        foreach (Consulta consulta in consultasEntities)
        {
          consulta.Paciente = _baseContext.Pacientes.Find(consulta.PacienteId);
          consulta.Medico = _baseContext.Profissionais.Find(consulta.ProfissionalId);
          consulta.Paciente.Pessoa = _baseContext.Pessoas.Find(consulta.Paciente.PessoaId);
          consulta.Medico.Pessoa = _baseContext.Pessoas.Find(consulta.Medico.PessoaId);
          consulta.Medico.Usuario = _baseContext.Usuarios.Find(consulta.Medico.UsuarioId);
          listConsultaModel.Add(await ConsultaModelByConsulta(consulta));
        }

        return listConsultaModel;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return new List<ConsultaModel>();
      }
    }

    public async Task<bool> CreateConsulta(ConsultaModel consultaModel)
    {
      ValidaAgenda(consultaModel);

      Consulta consulta = ConsultaByConsultaModel(consultaModel);
      consulta = CarregaDadosConsulta(consulta, consultaModel);

      try
      {
        await _consultaRepository.AddAsync(consulta);
        await _baseContext.SaveChangesAsync();

        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }

    private Consulta CarregaDadosConsulta(Consulta consulta, ConsultaModel consultaModel)
    {
      consulta.Paciente = _baseContext.Pacientes.SingleOrDefault<Paciente>(p => p.Id == consultaModel.Paciente.Id);
      consulta.Paciente.Pessoa = _baseContext.Pessoas.SingleOrDefault<Pessoa>(p => p.Id == consulta.Paciente.PessoaId);
      consulta.Medico = _baseContext.Profissionais.SingleOrDefault<Profissional>(c => c.Id == consultaModel.Medico.Id);
      consulta.Medico.Pessoa = _baseContext.Pessoas.SingleOrDefault<Pessoa>(p => p.Id == consulta.Medico.PessoaId);
      consulta.Medico.Usuario = _baseContext.Usuarios.SingleOrDefault<Usuario>(u => u.Id == consulta.Medico.UsuarioId);
      return consulta;
    }

    private void ValidaAgenda(ConsultaModel consultaModel)
    {
      Consulta consultaMesmoHorario = _baseContext.Consultas.SingleOrDefault<Consulta>(c => c.Excluido == false &&
        c.ProfissionalId == consultaModel.Medico.Id &&
        c.Data == consultaModel.Data &&
        c.Horario == consultaModel.Horario.Value);

      if (consultaModel.Id > 0)
        throw new Exception("Agenda já está alocada para outro paciente.");
    }

    private Consulta ConsultaByConsultaModel(ConsultaModel consultaModel)
    {
      return new Consulta()
      {
        Data = consultaModel.Data,
        Id = consultaModel.Id,
        Horario = consultaModel.Horario.Value,
        Medico = new Profissional()
        {
          CRM = consultaModel.Medico.CRM,
          Id = consultaModel.Medico.Id,
          NumeroCarteiraTrabalho = consultaModel.Medico.NumeroCarteiraTrabalho,
          Tipo = consultaModel.Medico.Tipo,
          Pessoa = new Pessoa()
          {
            CPF = consultaModel.Medico.CPF,
            DataNascimento = consultaModel.Medico.DataNascimento,
            Email = consultaModel.Medico.Email,
            Nome = consultaModel.Medico.Nome,
            Telefone = consultaModel.Medico.Telefone,
            Id = consultaModel.Medico.Id
          },
          PessoaId = consultaModel.Medico.IdPessoa,
          Usuario = new Usuario()
          {
            Id = consultaModel.Medico.Usuario.Id,
            Login = consultaModel.Medico.Usuario.Login,
            Senha = consultaModel.Medico.Usuario.Senha
          },
          UsuarioId = consultaModel.Medico.Usuario.Id
        },
        ProfissionalId = consultaModel.Medico.Id,
        Paciente = new Paciente()
        {
          Id = consultaModel.Paciente.Id,
          NomeConvenio = consultaModel.Paciente.NomeConvenio,
          NumeroCarteirinha = consultaModel.Paciente.NumeroCarteirinha,
          PossuiConvenio = consultaModel.Paciente.PossuiConvenio,
          Pessoa = new Pessoa()
          {
            CPF = consultaModel.Paciente.CPF,
            DataNascimento = consultaModel.Paciente.DataNascimento,
            Email = consultaModel.Paciente.Email,
            Nome = consultaModel.Paciente.Nome,
            Telefone = consultaModel.Paciente.Telefone,
            Id = consultaModel.Paciente.Id
          },
          PessoaId = consultaModel.Paciente.IdPessoa
        },
        PacienteId = consultaModel.Paciente.Id
      };
    }

    public async Task<bool> Delete(int id)
    {
      try
      {
        Consulta consulta = _baseContext.Consultas.SingleOrDefault<Consulta>(p => p.Id == id);
        consulta.Excluido = true;

        await _consultaRepository.UpdateAsync(consulta);
        await _baseContext.SaveChangesAsync();
        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }

    public async Task<bool> UpdateConsulta(ConsultaModel consultaModel)
    {
      Consulta consulta = ConsultaByConsultaModel(consultaModel);

      try
      {
        await _consultaRepository.UpdateAsync(consulta);
        await _baseContext.SaveChangesAsync();

        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }
  }
}
