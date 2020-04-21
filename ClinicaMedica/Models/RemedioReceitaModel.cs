using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
    public class RemedioReceitaModel
    {
        public RemedioReceitaModel()
        {
            Receita = new ReceitaModel();
            Remedio = new RemedioModel();
        }
        public int Id { get; set; }
        public ReceitaModel Receita { get; set; }
        public RemedioModel Remedio { get; set; }
        public string Observacoes { get; set; }
    }
}
