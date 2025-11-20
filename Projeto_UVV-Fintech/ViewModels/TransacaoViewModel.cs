using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.ViewModels
{
    public class TransacaoViewModel
    {

        
        public int ID { get; set; }
        public double valor { get; set; }
        public string tipoTransacao { get; set; }
        public int? NumeroContaRemetente { get; set; }
        public int? NumeroContaDestinatario { get; set; }
        public DateTime? DataHoraTransacao { get; set; }

    }
}
