using Microsoft.EntityFrameworkCore;
using Projeto_UVV_Fintech.Banco_Dados.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.Repository
{
    internal class TransacaoRepository
    {
        public static bool CriarTransacao(
            TipoTransacao tipo,
            double valor,
            int? contaRemetenteId,
            int? contaDestinatarioId)
        {
            using var context = new DB_Context();

            Conta contaRemetente = null;
            Conta contaDestinatario = null;

            if (contaRemetenteId != null)
                contaRemetente = context.Contas.FirstOrDefault(c => c.Id == contaRemetenteId);

            if (contaDestinatarioId != null)
                contaDestinatario = context.Contas.FirstOrDefault(c => c.Id == contaDestinatarioId);

            // Criar entidade final
            var novaTransacao = new Transacao
            {
                Tipo = tipo,
                Valor = valor,
                ContaRemetenteId = contaRemetenteId,
                ContaDestinatarioId = contaDestinatarioId,
                ContaRemetente = contaRemetente,
                ContaDestinatario = contaDestinatario,
                DataHoraTransacao = DateTime.Now
            };

            context.Transacoes.Add(novaTransacao);
            context.SaveChanges();
            return true;
        }

        public static List<Transacao> ListarTransacoes()
        {
            using var context = new DB_Context();

            return context.Transacoes
                .Include(t => t.ContaRemetente)
                .Include(t => t.ContaDestinatario)
                .ToList();
        }

        public static List<Transacao> FiltrarTransacoes(
            int? idTransacao,
            int? contaRemetente,
            int? contaDestinatario,
            string? tipo,
            double? valor,
            DateTime? dataTransacao,
            bool? valorMaior,
            bool? dataMaior)
        {
            var transacoes = ListarTransacoes();

            var filtrado = transacoes
                .Where(t =>
                    (idTransacao == null || t.Id == idTransacao) &&
                    (contaRemetente == null || t.ContaRemetenteId == contaRemetente) &&
                    (contaDestinatario == null || t.ContaDestinatarioId == contaDestinatario) &&
                    (string.IsNullOrWhiteSpace(tipo) || t.Tipo.ToString().Contains(tipo, StringComparison.OrdinalIgnoreCase)) &&
                    (
                        valor == null ||
                        (
                            valorMaior == true ? t.Valor >= valor :
                            valorMaior == false ? t.Valor <= valor :
                            true
                        )
                    ) &&
                    (
                        dataTransacao == null ||
                        (
                            dataMaior == true ? t.DataHoraTransacao >= dataTransacao :
                            dataMaior == false ? t.DataHoraTransacao <= dataTransacao :
                            true
                        )
                    )
                )
                .ToList();

            return filtrado;
        }
    }
}