using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.Model
{
    internal class ContaPoupanca :Conta
    {
        public const double TAXA_RENDIMENTO = 0.03; // 3% ao mês
        public const double TAXA_SAQUE = 0.01;      // 1% após 2 saques
        public const int SAQUES_GRATIS = 2;
        public int SaquesRealizadosNoMes { get; set; } = 0;

        public ContaPoupanca() { }

        public ContaPoupanca(double Saldo, int ClienteId, ICollection<Transacao> transacoes)
        {
            Saldo = this.Saldo;
            ClienteId = this.ClienteId;
            Transacoes = transacoes;
        }
    }
}
