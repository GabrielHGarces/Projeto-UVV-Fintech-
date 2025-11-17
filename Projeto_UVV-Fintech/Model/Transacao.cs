using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.Model
{
    public enum TipoTransacao
    {
        Saque,
        Deposito,
        Transferencia
    }
    internal class Transacao
    {
        public int Id { get; set; }
        public TipoTransacao Tipo { get; set; }
        public double Valor { get; set; }
        public int? RemetenteId { get; set; }

        public int? DestinatarioId { get; set; }
        public DateTime DataHoraTransacao { get; set; }
        public int ContaId { get; set; }

        public Conta Conta { get; set; } = null!;


        public Transacao() { }

        //Construtor Saque, Depósito Tranferencia. int? permite NULL assim:
        //Saque : Destinatário =NULLL
        //Deposito: Remetente = NULL;
        //Transferencia: Ambos Preenchidos.
        public Transacao(TipoTransacao tipo, double valor, int? remetenteId, int? destinatarioId, int contaId)
        {
            Tipo = tipo;
            Valor = valor;
            RemetenteId = remetenteId;
            DestinatarioId = destinatarioId;
            ContaId = contaId;
        }

    }
}
