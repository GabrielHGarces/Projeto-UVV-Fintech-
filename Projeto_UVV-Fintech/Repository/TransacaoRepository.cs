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

            // Ajuste dos nomes para coincidir com o enum
            if (tipo == "Depósito")
                tipo = "Deposito";
            else if (tipo == "Transferência")
                tipo = "Transferencia";

            var filtrado = transacoes
                .Where(t =>
                    // Filtrar pelo ID da transação
                    (idTransacao == null || t.Id == idTransacao) &&

                    // 🔹 Filtrar por Número da Conta Remetente
                    (contaRemetente == null ||
                        (t.ContaRemetente != null &&
                         t.ContaRemetente.NumeroConta == contaRemetente)) &&

                    // 🔹 Filtrar por Número da Conta Destinatário
                    (contaDestinatario == null ||
                        (t.ContaDestinatario != null &&
                         t.ContaDestinatario.NumeroConta == contaDestinatario)) &&

                    // Tipo de transação
                    (string.IsNullOrWhiteSpace(tipo) ||
                     tipo == "Todos" ||
                     t.Tipo.ToString().Contains(tipo, StringComparison.OrdinalIgnoreCase)) &&

                    // Valor da transação
                    (
                        valor == null ||
                        (
                            valorMaior == true ? t.Valor >= valor :
                            valorMaior == false ? t.Valor <= valor :
                            true
                        )
                    ) &&

                    // Data da transação
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