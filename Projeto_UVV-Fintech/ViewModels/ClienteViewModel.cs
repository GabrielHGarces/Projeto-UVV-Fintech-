using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.ViewModels
{
    public class ClienteViewModel
    {
        public int ClientId { get; set; }
        public string NomeCliente { get; set; }
        public string Telefone { get; set; } 
        public string CEP { get; set; } 
       
        
        public DateTime? DataAdesao { get; set; }
        public int NumeroContas { get; set; }
    }
}
