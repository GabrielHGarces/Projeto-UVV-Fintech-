using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.Model
{
    internal class Conta
    {
        public int Id { get; set; }
        public double Saldo { get; set; }
        public DateTime DataCriacao { get; set; }
        public int ClienteId { get; set; } // Chave estrangeira para o Cliente
        public ICollection<Transacao> Transacoes { get; set; }

        //Conta é uma classe abstrata Então ela não é instanciada
        
        //Função Relacional 1 cliente pode ter uma ou até 2 contas.
        public bool LigarConta(int IdCliente)
        {


            return false;
        }
        
    }
}
