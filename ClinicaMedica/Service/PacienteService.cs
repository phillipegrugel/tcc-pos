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
    public class PacienteService : ServiceBase, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly BaseContext _baseContext;

        public PacienteService(IPacienteRepository pacienteRepository, IPessoaRepository pessoaRepository, BaseContext baseContext)
        {
            _pacienteRepository = pacienteRepository;
            _pessoaRepository = pessoaRepository;
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

        public async Task<dynamic> CreatePaciente(PacienteModel pacienteModel)
        {
            Pessoa pessoaMesmoCPF = _baseContext.Pessoas.FirstOrDefault(p => p.Excluido == false && p.CPF == pacienteModel.CPF);

            if (pessoaMesmoCPF != null)
            {
                Paciente pacienteMesmoCPF = _baseContext.Pacientes.FirstOrDefault(p => p.Excluido == false && p.PessoaId == pessoaMesmoCPF.Id);

                if (pacienteMesmoCPF != null)
                {
                    return GeraRetornoError($"O CPF {pacienteModel.CPF} já está cadastrado para o Paciente {pessoaMesmoCPF.Nome}");
                }
            }

            Task<dynamic> validacoes = await Validacoes(pacienteModel);

            if (validacoes != null)
                return validacoes;

            Paciente paciente = PacienteByPacienteModel(pacienteModel);

            try
            {
                await _pacienteRepository.AddAsync(paciente);
                await _baseContext.SaveChangesAsync();

                return GeraRetornoSucess("Paciente cadastrado.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private async Task<dynamic> Validacoes(PacienteModel pacienteModel)
        {

            if (!IsCpf(pacienteModel.CPF))
            {
                return GeraRetornoError("CPF inválido.");
            }

            if (pacienteModel.PossuiConvenio && (string.IsNullOrEmpty(pacienteModel.NomeConvenio) || string.IsNullOrEmpty(pacienteModel.NumeroCarteirinha)))
            {
                return GeraRetornoError("Você informou que o paciente possui convênio, então será necessário informar os dados de nome do convênio e o número da carteirinha.");
            }

            if (pacienteModel.DataNascimento == DateTime.MinValue)
            {
                return GeraRetornoNullError("data de nascimento");
            }

            return null;
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

        public async Task<dynamic> Delete(int id)
        {
            try
            {
                List<Consulta> consultas = _baseContext.Consultas.Where(c => c.Excluido == false && c.PacienteId == id).ToList();

                if (consultas.Count > 0)
                {
                    return GeraRetornoError("O paciente não será excluído pois possui consulta cadastrada.");
                }

                Paciente paciente = _baseContext.Pacientes.SingleOrDefault<Paciente>(p => p.Id == id);
                paciente.Excluido = true;
                paciente.Pessoa = _baseContext.Pessoas.SingleOrDefault<Pessoa>(p => p.Id == paciente.PessoaId);
                paciente.Pessoa.Excluido = true;

                await _pacienteRepository.UpdateAsync(paciente);
                await _baseContext.SaveChangesAsync();
                return GeraRetornoSucess("Paciente excluído.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<dynamic> UpdatePaciente(PacienteModel pacienteModel)
        {
            Task<dynamic> validacoes = await Validacoes(pacienteModel);

            if (validacoes != null)
                return validacoes;

            Paciente paciente = PacienteByPacienteModel(pacienteModel);

            try
            {
                await _pacienteRepository.UpdateAsync(paciente);
                await _pessoaRepository.UpdateAsync(paciente.Pessoa);
                await _baseContext.SaveChangesAsync();

                return GeraRetornoSucess("Paciente alterado.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
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
