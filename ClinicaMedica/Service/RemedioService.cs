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
    public class RemedioService : ServiceBase, IRemedioService
    {
        private readonly IRemedioRepository _remedioRepository;
        private readonly BaseContext _baseContext;

        public RemedioService(IRemedioRepository remedioRepository, BaseContext baseContext)
        {
            _remedioRepository = remedioRepository;
            _baseContext = baseContext;
        }
        public async Task<RemedioModel> BuscaRemedio(int id)
        {
            try
            {
                Remedio remedio = _baseContext.Remedios.SingleOrDefault<Remedio>(p => p.Id == id && p.Excluido == false);
                return await RemedioModelByRemedio(remedio);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new RemedioModel();
            }
        }

        private async Task<RemedioModel> RemedioModelByRemedio(Remedio remedio)
        {
            return new RemedioModel()
            {
                Fabricante = remedio.Fabricante,
                Id = remedio.Id,
                Nome = remedio.Nome,
                NomeGenerico = remedio.NomeGenerico
            };
        }

        public async Task<List<RemedioModel>> BuscaRemedios()
        {
            try
            {
                List<Remedio> remediosEntities = _baseContext.Remedios.Where(p => p.Excluido == false).ToList();
                List<RemedioModel> listRemedioModel = new List<RemedioModel>();
                foreach (Remedio remedio in remediosEntities)
                {
                    listRemedioModel.Add(await RemedioModelByRemedio(remedio));
                }

                return listRemedioModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<RemedioModel>();
            }
        }

        public async Task<dynamic> CreateRemedio(RemedioModel remedioModel)
        {
            Remedio remedio = RemedioByRemedioModel(remedioModel);

            try
            {
                await _remedioRepository.AddAsync(remedio);
                await _baseContext.SaveChangesAsync();

                return await GeraRetornoSucess("Remédio cadastrado.");
            }
            catch
            {
                return await GeraRetornoError();
            }
        }

        private Remedio RemedioByRemedioModel(RemedioModel remedioModel)
        {
            return new Remedio()
            {
                NomeGenerico = remedioModel.NomeGenerico,
                Fabricante = remedioModel.Fabricante,
                Id = remedioModel.Id,
                Nome = remedioModel.Nome
            };
        }

        public async Task<dynamic> Delete(int id)
        {
            try
            {
                Remedio remedio = _baseContext.Remedios.SingleOrDefault<Remedio>(p => p.Id == id);
                remedio.Excluido = true;

                await _remedioRepository.UpdateAsync(remedio);
                await _baseContext.SaveChangesAsync();
                return await GeraRetornoSucess("Remédio excluído.");
            }
            catch
            {
                return await GeraRetornoError();
            }
        }

        public async Task<dynamic> UpdateRemedio(RemedioModel remedioModel)
        {
            Remedio remedio = RemedioByRemedioModel(remedioModel);

            try
            {
                await _remedioRepository.UpdateAsync(remedio);
                await _baseContext.SaveChangesAsync();

                return await GeraRetornoSucess("Remédio alterado.");
            }
            catch
            {
                return await GeraRetornoError();
            }
        }
    }
}
