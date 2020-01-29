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
  public class PacienteService : IPacienteService
  {
    private readonly IPacienteRepository _pacienteRepository;
    private readonly BaseContext _baseContext;

    public PacienteService(IPacienteRepository pacienteRepository, BaseContext baseContext)
    {
      _pacienteRepository = pacienteRepository;
      _baseContext = baseContext;
    }
    public async Task<PacienteModel> BuscaPaciente(int id)
    {
      try
      {
        Paciente paciente = _baseContext.Pacientes.SingleOrDefault<Paciente>(p => p.Id == id && p.Excluido == false);
        paciente.Pessoa = _baseContext.Pessoas.SingleOrDefault<Pessoa>(p => p.Id == paciente.PessoaId && p.Excluido == false);
        return await PacienteModelByPaciente(paciente);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return new PacienteModel();
      }
    }

    private async Task<PacienteModel> PacienteModelByPaciente(Paciente paciente)
    {
      return new PacienteModel()
      {
        Id = paciente.Id,
        CPF = paciente.Pessoa.CPF,
        DataNascimento = paciente.Pessoa.DataNascimento,
        Email = paciente.Pessoa.Email,
        IdPessoa = paciente.PessoaId,
        Nome = paciente.Pessoa.Nome,
        NomeConvenio = paciente.NomeConvenio,
        NumeroCarteirinha = paciente.NumeroCarteirinha,
        PossuiConvenio = paciente.PossuiConvenio,
        Telefone = paciente.Pessoa.Telefone
      };
    }

    public async Task<List<PacienteModel>> BuscaPacientes()
    {
      try
      {
        List<Paciente> pacientesEntities = _baseContext.Pacientes.Where(p => p.Excluido == false).ToList();
        List<PacienteModel> listPacienteModel = new List<PacienteModel>();
        foreach (Paciente paciente in pacientesEntities)
        {
          paciente.Pessoa = _baseContext.Pessoas.Find(paciente.PessoaId);
          listPacienteModel.Add(await PacienteModelByPaciente(paciente));
        }

        return listPacienteModel;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return new List<PacienteModel>();
      }
    }

    public async Task<bool> CreatePaciente(PacienteModel pacienteModel)
    {
      Paciente paciente = PacienteByPacienteModel(pacienteModel);

      try
      {
        await _pacienteRepository.AddAsync(paciente);
        await _baseContext.SaveChangesAsync();

        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }

    private Paciente PacienteByPacienteModel(PacienteModel pacienteModel)
    {
      return new Paciente()
      {
        Id = pacienteModel.Id,
        NomeConvenio = pacienteModel.NomeConvenio,
        NumeroCarteirinha = pacienteModel.NumeroCarteirinha,
        Pessoa = new Pessoa()
        {
          CPF = pacienteModel.CPF,
          DataNascimento = pacienteModel.DataNascimento,
          Id = pacienteModel.IdPessoa,
          Email = pacienteModel.Email,
          Nome = pacienteModel.Nome,
          Telefone = pacienteModel.Telefone
        },
        PessoaId = pacienteModel.IdPessoa,
        PossuiConvenio = pacienteModel.PossuiConvenio
      };
    }

    public async Task<bool> Delete(int id)
    {
      try
      {
        Paciente paciente = _baseContext.Pacientes.SingleOrDefault<Paciente>(p => p.Id == id);
        paciente.Excluido = true;
        paciente.Pessoa = _baseContext.Pessoas.SingleOrDefault<Pessoa>(p => p.Id == paciente.PessoaId);
        paciente.Pessoa.Excluido = true;

        await _pacienteRepository.UpdateAsync(paciente);
        await _baseContext.SaveChangesAsync();
        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }

    public async Task<bool> UpdatePaciente(PacienteModel pacienteModel)
    {
      Paciente paciente = PacienteByPacienteModel(pacienteModel);

      try
      {
        await _pacienteRepository.UpdateAsync(paciente);
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
