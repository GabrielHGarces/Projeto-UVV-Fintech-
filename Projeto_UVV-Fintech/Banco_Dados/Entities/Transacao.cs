using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projeto_UVV_Fintech.Banco_Dados.Entities
{
    public enum TipoTransacao
    {
        Saque,
        Deposito,
        Transferencia
    }
    public class Transacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public TipoTransacao Tipo { get; set; }

        [Required]
        public double Valor { get; set; }

        // Conta que envia
        public int? ContaRemetenteId { get; set; }
        [ForeignKey(nameof(ContaRemetenteId))]
        public Conta ContaRemetente { get; set; }

        
        // Conta que recebe
        public int? ContaDestinatarioId { get; set; }
        [ForeignKey(nameof(ContaDestinatarioId))]
        public Conta ContaDestinatario { get; set; }

        [Required]
        public DateTime DataHoraTransacao { get; set; }

        public Transacao() { }
    }

}

