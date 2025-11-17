using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.Model
{
    internal class ContaCorrente :Conta
    {

        public const double TAXA_MANUTENCAO = 4.0;
        public ContaCorrente() { }
        
        public ContaCorrente(double Saldo,int ClienteId, ICollection<Transacao> transacoes)
        {
            Saldo = this.Saldo;
            ClienteId = this.ClienteId;
            Transacoes = transacoes;
        }

    }
}
