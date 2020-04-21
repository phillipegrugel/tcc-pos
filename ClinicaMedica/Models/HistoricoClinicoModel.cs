using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Models
{
  public class HistoricoClinicoModel
  {
    public int Id { get; set; }
    public ConsultaModel Consulta { get; set; }
    public List<PedidoExameModel> Exames { get; set; }
    public ReceitaModel Receita { get; set; }
    public string Observacao { get; set; }
  }
}
