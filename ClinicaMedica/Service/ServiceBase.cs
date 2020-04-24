using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Service
{
    public class ServiceBase
    {
        public async Task<dynamic> GeraRetornoSucess(string mensagem)
        {
            return new
            {
                error = false,
                mensagem
            };
        }

        public async Task<dynamic> GeraRetornoError(string mensagem = "Ocorreu um erro ao processar sua solicitação, por favor entre em contato com o administrador do sistema.")
        {
            return new
            {
                error = true,
                mensagem
            };
        }
    }
}
