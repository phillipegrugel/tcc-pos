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
    public class ConsultaService : ServiceBase, IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly BaseContext _baseContext;
        private readonly IProfissionalService _profissionalService;

        public ConsultaService(IConsultaRepository consultaRepository, BaseContext baseContext, IProfissionalService profissionalService)
        {
            _consultaRepository = consultaRepository;
            _baseContext = baseContext;
            _profissionalService = profissionalService;
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

        private ConsultaModel ConsultaModelByConsultaAsync(Consulta consulta)
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

            ConsultaModel consultaModel = new ConsultaModel()
            {
                Id = consulta.Id,
                Data = consulta.Data,
                HistoricoClinico = null,
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

            return consultaModel;
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
                HistoricoClinico = GetHistoricoClinico(consulta.Id, false),
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

        public HistoricoClinicoModel GetHistoricoClinico(int id, bool addConsultaModel)
        {
            HistoricoClinico historicoClinico = _baseContext.HistoricosClinicos.FirstOrDefault<HistoricoClinico>(h => h.ConsultaId == id);

            if (historicoClinico == null)
            {
                HistoricoClinicoModel retornoVazio = new HistoricoClinicoModel();
                retornoVazio.Exames = new List<PedidoExameModel>();
                retornoVazio.Receita = new ReceitaModel();
                //retornoVazio.Receita.Remedios.Add(new RemedioReceitaModel { Remedio = new RemedioModel { Nome = "Pantoprazol" }, Observacoes = "1 vez por dia." });
                return retornoVazio;
            }

            List<PedidoExame> pedidosDeExames = _baseContext.PedidosExames.Where(p => p.HistoricoClinicoId == historicoClinico.Id).ToList();
            Receita receita = _baseContext.Receitas.FirstOrDefault(r => r.HistoricoClinicoId == historicoClinico.Id);


            HistoricoClinicoModel historicoClinicoModel = new HistoricoClinicoModel
            {
                Id = historicoClinico.Id,
                Exames = GetListaPedidosExameModel(pedidosDeExames),
                Observacao = historicoClinico.Observacoes,
                Receita = GetReceotaModel(receita)
            };

            if (addConsultaModel)
            {
                Consulta consulta = _baseContext.Consultas.FirstOrDefault(c => c.Id == id);
                consulta.Medico = _baseContext.Profissionais.FirstOrDefault(m => m.Id == consulta.ProfissionalId);
                consulta.Medico.Pessoa = _baseContext.Pessoas.FirstOrDefault(p => p.Id == consulta.Medico.PessoaId);
                consulta.Medico.Usuario = _baseContext.Usuarios.SingleOrDefault<Usuario>(u => u.Id == consulta.Medico.UsuarioId);
                consulta.Paciente = _baseContext.Pacientes.FirstOrDefault(p => p.Id == consulta.PacienteId);
                consulta.Paciente.Pessoa = _baseContext.Pessoas.FirstOrDefault(p => p.Id == consulta.Paciente.PessoaId);
                historicoClinicoModel.Consulta = ConsultaModelByConsultaAsync(consulta);
            }

            return historicoClinicoModel;
        }

        private ReceitaModel GetReceotaModel(Receita receita)
        {
            return new ReceitaModel
            {
                Id = receita.Id,
                Observacoes = receita.Observacoes,
                Remedios = GetListaRemedioReceitaModel(receita.Id)
            };
        }

        private List<RemedioReceitaModel> GetListaRemedioReceitaModel(int id)
        {
            List<RemedioReceitaModel> listRemedioReceitaModel = new List<RemedioReceitaModel>();

            List<RemedioReceita> remedioReceitas = _baseContext.RemedioReceitas.Where(r => r.ReceitaId == id).ToList();

            foreach (RemedioReceita remedioReceita in remedioReceitas)
            {
                listRemedioReceitaModel.Add(new RemedioReceitaModel
                {
                    Id = remedioReceita.Id,
                    Observacoes = remedioReceita.Observacoes,
                    Remedio = GetRemedioModel(remedioReceita.RemedioId)
                });
            }

            return listRemedioReceitaModel;
        }

        private RemedioModel GetRemedioModel(int remedioId)
        {
            Remedio remedio = _baseContext.Remedios.FirstOrDefault(r => r.Id == remedioId);

            return new RemedioModel
            {
                Id = remedio.Id,
                Fabricante = remedio.Fabricante,
                Nome = remedio.Nome,
                NomeGenerico = remedio.NomeGenerico
            };
        }

        private List<PedidoExameModel> GetListaPedidosExameModel(List<PedidoExame> pedidoExames)
        {
            List<PedidoExameModel> listPedidoExameModels = new List<PedidoExameModel>();

            foreach (PedidoExame pedido in pedidoExames)
            {
                PedidoExameModel pedidoModel = new PedidoExameModel
                {
                    Id = pedido.Id,
                    EntreguePaciente = pedido.EntreguePaciente,
                    Resultado = pedido.Resultado,
                    Exame = GetExameModel(pedido.ExameId)
                };

                listPedidoExameModels.Add(pedidoModel);
            }

            return listPedidoExameModels;
        }

        private ExameModel GetExameModel(int exameId)
        {
            Exame exame = _baseContext.Exames.FirstOrDefault(e => e.Id == exameId);
            return new ExameModel
            {
                Id = exame.Id,
                Nome = exame.Nome
            };
        }

        public async Task<List<ConsultaModel>> BuscaConsultas(string login)
        {
            try
            {
                Usuario usuario = _baseContext.Usuarios.FirstOrDefault(u => u.Login == login && u.Excluido == false);
                List<Consulta> consultasEntities;
                if (usuario.Role == "medico")
                {
                    Profissional medico = _baseContext.Profissionais.FirstOrDefault(m => m.Excluido == false && m.UsuarioId == usuario.Id);
                    consultasEntities = _baseContext.Consultas.Where(p => p.Excluido == false && p.ProfissionalId == medico.Id).ToList();
                }
                else
                    consultasEntities = _baseContext.Consultas.Where(p => p.Excluido == false).ToList();

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

        public async Task<dynamic> CreateConsulta(ConsultaModel consultaModel)
        {
            dynamic validacoes  = ValidaAgenda(consultaModel);

            if (validacoes != null)
                return validacoes;

            Consulta consulta = ConsultaByConsultaModel(consultaModel);
            consulta = CarregaDadosConsulta(consulta, consultaModel);

            try
            {
                await _consultaRepository.AddAsync(consulta);
                await _baseContext.SaveChangesAsync();

                return await GeraRetornoSucess("Consulta agendada.");
            }
            catch
            {
                return await GeraRetornoError();
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

        private dynamic ValidaAgenda(ConsultaModel consultaModel)
        {
            Consulta consultaMesmoHorario = _baseContext.Consultas.SingleOrDefault<Consulta>(c => c.Excluido == false &&
              c.ProfissionalId == consultaModel.Medico.Id &&
              c.Data == consultaModel.Data &&
              c.Horario == consultaModel.Horario.Value);

            if (consultaModel.Id > 0)
                return GeraRetornoError("O horário selecionado não está disponível.");

            return null;
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

        public async Task<dynamic> Delete(int id)
        {
            try
            {
                Consulta consulta = _baseContext.Consultas.SingleOrDefault<Consulta>(p => p.Id == id);
                consulta.Excluido = true;

                await _consultaRepository.UpdateAsync(consulta);
                await _baseContext.SaveChangesAsync();
                return GeraRetornoSucess("Consulta cancelada.");
            }
            catch
            {
                return GeraRetornoError();
            }
        }

        public async Task<dynamic> UpdateConsulta(ConsultaModel consultaModel)
        {
            Consulta consulta = ConsultaByConsultaModel(consultaModel);

            try
            {
                await _consultaRepository.UpdateAsync(consulta);
                await _baseContext.SaveChangesAsync();

                return GeraRetornoSucess("Consulta alterada.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<dynamic> SalvarHistorico(ConsultaModel consultaModel)
        {
            try
            {
                HistoricoClinico historicoClinico = GetHistoricoClinicoByHistoricoClinicoModel(consultaModel);
                _baseContext.HistoricosClinicos.Add(historicoClinico);
                _baseContext.SaveChanges();
                return GeraRetornoSucess("Histórico clinico cadastrado.");
            }
            catch
            {
                return GeraRetornoError();
            }
        }

        private Receita GetReceitaByHistoricoClinicoModel(HistoricoClinicoModel historicoClinico)
        {
            return new Receita
            {
                Observacoes = historicoClinico.Receita.Observacoes,
                Remedios = GetListaRemedioReceitaByReceitaModel(historicoClinico.Receita)
            };
        }

        private List<RemedioReceita> GetListaRemedioReceitaByReceitaModel(ReceitaModel receita)
        {
            List<RemedioReceita> listaRemedioReceita = new List<RemedioReceita>();

            foreach (RemedioReceitaModel remedioReceita in receita.Remedios)
            {
                listaRemedioReceita.Add(new RemedioReceita
                {
                    Observacoes = remedioReceita.Observacoes,
                    //Remedio = GetRemedioByRemedioModel(remedioReceita.Remedio),
                    RemedioId = remedioReceita.Remedio.Id
                });
            }

            return listaRemedioReceita;
        }

        private Remedio GetRemedioByRemedioModel(RemedioModel remedio)
        {
            return new Remedio
            {

                Id = remedio.Id,
                Fabricante = remedio.Fabricante,
                Nome = remedio.Nome,
                NomeGenerico = remedio.NomeGenerico
            };
        }

        private HistoricoClinico GetHistoricoClinicoByHistoricoClinicoModel(ConsultaModel consultaModel)
        {
            return new HistoricoClinico
            {
                Consulta = _baseContext.Consultas.FirstOrDefault(c => c.Id == consultaModel.Id),
                ConsultaId = consultaModel.Id,
                Observacoes = consultaModel.HistoricoClinico.Observacao,
                Receita = GetReceitaByHistoricoClinicoModel(consultaModel.HistoricoClinico),
                Exames = GetListExamesByListExamesModel(consultaModel.HistoricoClinico.Exames)
            };
        }

        private List<PedidoExame> GetListExamesByListExamesModel(List<PedidoExameModel> exames)
        {
            List<PedidoExame> listPedidosExames = new List<PedidoExame>();

            foreach (PedidoExameModel pedido in exames)
            {
                listPedidosExames.Add(new PedidoExame
                {
                    EntreguePaciente = pedido.EntreguePaciente,
                    //Exame = GetExamesByExameModel(pedido.Exame),
                    ExameId = pedido.Exame.Id,
                    Resultado = pedido.Resultado
                });
            }

            return listPedidosExames;
        }

        private Exame GetExamesByExameModel(ExameModel exame)
        {
            return new Exame
            {
                Id = exame.Id,
                Nome = exame.Nome
            };
        }

        public async Task<dynamic> GeraConsultaRapida(int idPaciente)
        {
            try
            {
                DateTime data = DateTime.Now.Date;
                List<Profissional> medicos = _baseContext.Profissionais.Where(p => p.Tipo == TipoProfissional.Medico).ToList();
                bool encontrou = false;
                DateTime melhorHorario = new DateTime();
                Profissional medicoSelecionado = null;
                HorarioModel horarioSelecionado = null;

                while (!encontrou)
                {
                    foreach (Profissional medico in medicos)
                    {
                        List<HorarioModel> horarios = _profissionalService.BuscaHorariosDisponiveisMedico(medico.Id, data);
                        foreach (HorarioModel horario in horarios)
                        {
                            DateTime diaHora = data;
                            diaHora = diaHora.AddHours(horario.Value + 8);

                            if (diaHora < DateTime.Now)
                                continue;

                            if (melhorHorario == DateTime.MinValue || melhorHorario > diaHora)
                            {
                                melhorHorario = diaHora;
                                medicoSelecionado = medico;
                                horarioSelecionado = horario;
                            }

                            break;
                        }
                    }

                    data = data.AddDays(1);

                    if (medicoSelecionado != null)
                        encontrou = true;
                }

                Consulta consulta = new Consulta
                {
                    Data = melhorHorario.Date,
                    Horario = horarioSelecionado.Value,
                    Excluido = false,
                    ProfissionalId = medicoSelecionado.Id,
                    PacienteId = idPaciente
                };

                _baseContext.Consultas.Add(consulta);
                _baseContext.SaveChanges();

                return $"Consulta criada para o dia {consulta.Data.ToString("dd/MM/yyyy")}, no horário {horarioSelecionado.Label}.";
            }
            catch
            {
                return "Error";
            }
        }
    }
}
