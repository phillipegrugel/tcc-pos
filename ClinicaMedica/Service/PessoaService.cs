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
  public class PessoaService : IPessoaService
  {
    public readonly IPessoaRepository _pessoaRepository;
    public readonly BaseContext _context;

    public PessoaService(IPessoaRepository pessoaRepository, BaseContext context)
    {
      _pessoaRepository = pessoaRepository;
      _context = context;
    }

    public async Task<bool> CreatePessoa(PessoaModel pessoaModel)
    {
      Pessoa pessoa = PessoaByPessoaModel(pessoaModel);

      try
      {
        await _pessoaRepository.AddAsync(pessoa);
        await _context.SaveChangesAsync();
        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }

    public async Task<bool> CreatePessoa()
    {
      try
      {
        Pessoa pessoa = new Pessoa()
        {
          CPF = "09059366638",
          Nome = "Phillipe",
          DataNascimento = new DateTime(1989, 12, 25)
        };
        await _pessoaRepository.AddAsync(pessoa);
        await _context.SaveChangesAsync();
        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }

    private Pessoa PessoaByPessoaModel(PessoaModel pessoaModel)
    {
      return new Pessoa()
      {
        Id = pessoaModel.IdPessoa,
        CPF = pessoaModel.CPF,
        DataNascimento = pessoaModel.DataNascimento,
        Email = pessoaModel.Email,
        Nome = pessoaModel.Nome,
        Telefone = pessoaModel.Telefone
      };
    }
  }
}
