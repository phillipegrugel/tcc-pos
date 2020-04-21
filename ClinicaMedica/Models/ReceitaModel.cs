using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
    public class ReceitaModel
    {
        public ReceitaModel()
        {
            Remedios = new List<RemedioReceitaModel>();
        }
        public int Id { get; set; }
        public List<RemedioReceitaModel> Remedios { get; set; }
        public string Observacoes { get; set; }
    }
}
