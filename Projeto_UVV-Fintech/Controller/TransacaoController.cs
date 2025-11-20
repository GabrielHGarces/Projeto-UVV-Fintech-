using Projeto_UVV_Fintech.Banco_Dados.Entities;
using Projeto_UVV_Fintech.Repository;
using Projeto_UVV_Fintech.ViewModels;
using Projeto_UVV_Fintech.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Projeto_UVV_Fintech.Controller
{
    public class TransacaoController
    {
        private readonly ViewTransacoes _view;

        public TransacaoController(ViewTransacoes view)
        {
            _view = view;
        }

        //Comentários para evitar erros de compilação pela falta dos métodos em model/Conta.cs
        public bool CriarTransacao(double valor, string tipo, int contaRemetente, int contaDestinatario)
        {
            try
            {
                //if (TransacaoRepository.CriarTransacao(valor, tipo, contaRemetente, contaDestinatario))
                //{
                //    MessageBox.Show($"Transacao criada com sucesso:\n valor: {valor}\n Tipo: {tipo}\n Remetente: {contaRemetente}\n Destinatario: {contaDestinatario}");
                //    return true;
                //}
                return false;
            } catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar transacao: {ex.Message}");
                return false;
            }
        }

        public List<TransacaoViewModel> ListarTransacoes()
        {
            try
            {
                var transacoes = TransacaoRepository.ListarTransacoes();

                var transacoesViewModel = transacoes.Select(t => new TransacaoViewModel
                {
                    ID = t.Id,
                    valor = t.Valor,
                    tipoTransacao = t.Tipo.ToString(),

                    NumeroContaRemetente = t.ContaRemetente != null
                        ? t.ContaRemetente.NumeroConta
                        : 1,

                    NumeroContaDestinatario = t.ContaDestinatario != null
                        ? t.ContaDestinatario.NumeroConta
                        : 0,

                    DataHoraTransacao = t.DataHoraTransacao
                }).ToList();

                _view.TabelaTransacoes.ItemsSource = transacoesViewModel;
                return transacoesViewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao listar transações: {ex.Message}");
                return new List<TransacaoViewModel>();
            }
        }





        public List<TransacaoViewModel> FiltrarTransacoes(int? idTransacao, int? contaRemetente, int? contaDestinatario, string? tipo, double? valor, DateTime? dataTransacao, bool? valorMaior, bool? dataMaior)
        {
            try
            {
                List<Transacao> filtrado = TransacaoRepository.FiltrarTransacoes(
                idTransacao, contaRemetente, contaDestinatario,
                tipo, valor, dataTransacao, valorMaior, dataMaior);
                var transacoesViewModel = filtrado.Select(t => new TransacaoViewModel
                {
                    ID = t.Id,
                    valor = t.Valor,
                    tipoTransacao = t.Tipo.ToString(),

                    NumeroContaRemetente = t.ContaRemetente?.NumeroConta ?? 0,
                    NumeroContaDestinatario = t.ContaDestinatario?.NumeroConta ?? 0,

                    DataHoraTransacao = t.DataHoraTransacao
                }).ToList();

                _view.TabelaTransacoes.ItemsSource = transacoesViewModel;
                return transacoesViewModel;
            } catch (Exception ex)
            {
                MessageBox.Show($"Erro ao filtrar transacoes: {ex.Message}");
                return new List <TransacaoViewModel>();
            }
        }

        public void AbrirViewContas(int numeroConta)
        {
            _view.Hide();
            var window = new ViewContas(numeroConta) { Owner = _view };
            window.ShowDialog();
            _view.Close();
        }
    }
}
