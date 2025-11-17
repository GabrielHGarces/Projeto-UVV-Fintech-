using Projeto_UVV_Fintech.Banco_Dados.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.Model
{
    internal class ModelTransacao
    {
        public void InserirTransacao(TipoTransacao tipo, double valor, int? remetenteId, int? destinatarioId, int contaId)
        {
            using var context = new DB_Context();
            Transacao novo = new Transacao();
            novo.Tipo = tipo;
            novo.Valor = valor;
            novo.RemetenteId = remetenteId;
            novo.DestinatarioId = destinatarioId;
            novo.ContaId = contaId;

            context.Transacoes.Add(novo);
            context.SaveChanges();


        }
        
        
    }
    
    
}
