using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Projeto_UVV_Fintech.Banco_Dados.Entities;
using Projeto_UVV_Fintech.Repository;
using Projeto_UVV_Fintech.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Projeto_UVV_Fintech.Controller
{
    public class ContaController
    {
        private readonly ViewContas _view;

        public ContaController(ViewContas view)
        {
            _view = view;
        }

        //Comentários para evitar erros de compilação pela falta dos métodos em model/Conta.cs
        public bool CriarConta()
        {
            try
            {
                _view.Opacity = 0.5;
                var dialog = new ContaDialog(this) { Owner = _view };
                bool? resultado = dialog.ShowDialog();
                _view.Opacity = 1;

                if (resultado == true)
                {
                    ListarContas();
                    return true;
                }
                return false;
            } catch (Exception ex)
            {
                MessageBox.Show("Erro ao criar conta: " + ex.Message);
                return false;
            }
        }

        public bool SalvarConta(string idCliente, string tipoConta, string nomeCliente)
        {
            if (string.IsNullOrWhiteSpace(idCliente))
            {
                MessageBox.Show("O campo ID não pode estar vazio.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tipoConta))
            {
                MessageBox.Show("O campo Tipo de Conta não pode estar vazio.");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(nomeCliente))
            {
                MessageBox.Show("Adicione o id correto do Cliente.");
                return false;
            }

            int idClienteInt = int.Parse(idCliente);

            try
            {
                if (tipoConta == "CC")
                {
                    if (ContaCorrenteRepository.CriarConta(idClienteInt))
                    {
                        MessageBox.Show($"Conta criada com sucesso:\nId Cliente: {idClienteInt}\nTipo Conta: {tipoConta}");
                        return true;
                    }
                    return false;
                }
                else if (tipoConta == "CP")
                {
                    if (ContaPoupancaRepository.CriarConta(idClienteInt))
                    {
                        MessageBox.Show($"Conta criada com sucesso:\nId Cliente: {idClienteInt}\nTipo Conta: {tipoConta}");
                        return true;
                    }
                    return false;
                }
                return false;
            } catch (Exception ex)
            {
                MessageBox.Show("ID do Cliente inválido. Por favor, insira um número válido." + ex.Message);
                return false;
            }
        }

        public List<Conta> ListarContas()
        {
            try
            {
                List<Conta> resultadoPoupanca = ContaPoupancaRepository.ListarContas();
                List<Conta> resultadoCorrente = ContaCorrenteRepository.ListarContas();

                var todasContas = resultadoPoupanca.Concat(resultadoCorrente);
                var contasUnicas = todasContas
                    .GroupBy(c => c.Id)
                    .Select(g => g.First())
                    .ToList();

                _view.TabelaContas.ItemsSource = contasUnicas;
                return contasUnicas;
            } catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar contas: " + ex.Message);
                return [];
            }
        }

        public List<Conta> FiltrarContas(string? IdCliente, int? numerConta, int? numeroAgencia, string? tipoConta, string? nomeTitular, double? saldo, DateTime? dataCriacao, bool? saldoMaior, bool? dataMaior)
        {
            try
            {
                if (!int.TryParse(IdCliente, out int idClienteInt))
                {
                    return new List<Conta>();
                }

                List<Conta> resultado;
                if (tipoConta == "CP")
                {
                    resultado = ContaPoupancaRepository.FiltrarContas(
                    idClienteInt, numerConta, numeroAgencia, tipoConta,
                    nomeTitular, saldo, dataCriacao, saldoMaior, dataMaior);
                } else if (tipoConta == "CC")
                {
                    resultado = ContaCorrenteRepository.FiltrarContas(
                    idClienteInt, numerConta, numeroAgencia, tipoConta,
                    nomeTitular, saldo, dataCriacao, saldoMaior, dataMaior);
                } else
                {
                    List<Conta> resultadoPoupanca = ContaPoupancaRepository.FiltrarContas(
                    idClienteInt, numerConta, numeroAgencia, tipoConta,
                    nomeTitular, saldo, dataCriacao, saldoMaior, dataMaior);
                    List<Conta> resultadoCorrente = ContaCorrenteRepository.FiltrarContas(
                    idClienteInt, numerConta, numeroAgencia, tipoConta,
                    nomeTitular, saldo, dataCriacao, saldoMaior, dataMaior);
                    resultado = resultadoPoupanca.Concat(resultadoCorrente).ToList();
                }

                return resultado;
            } catch (Exception ex)
            {
                MessageBox.Show("Erro ao filtrar contas: " + ex.Message);
                return [];
            }
            
        }

        //public void sacar(Conta conta, double valor)
        //{
        //    if (conta.Sacar(valor))
        //    {
        //        MessageBox.Show("Valor de: R$" + valor + "Sacado com sucesso!");
        //    }
        //}

        //public void depositar(Conta conta, double valor)
        //{
        //    if (conta.Depositar(valor))
        //    {
        //        MessageBox.Show("Valor de: R$" + valor + "Depositado com sucesso!");
        //    }
        //}

        //public void transferir(Conta contaOrigem, Conta contaDestino, double valor)
        //{

        //    if (contaOrigem.Transferir(contaDestino, valor))
        //    {
        //        MessageBox.Show("Valor de: R$" + valor + "Transferido com sucesso para a conta " + contaDestino + "!");
        //    }
        //}

        //public static Conta? ObterContaPorId(int contaId)
        //{
        //    return Conta.ObterContaPorId(contaId);
        //}

        public string GetNomeClientePorId(string idCliente)
        {
            if (int.TryParse(idCliente, out int id))
            {
                var cliente = ClienteRepository.ObterClientePorId(id);
                return cliente?.Nome ?? string.Empty;
            }
            return string.Empty;
        }

        public void AbrirViewClientes(Conta contaSelecionada)
        {
            _view.Hide();
            var window = new ViewClientes(contaSelecionada.Id) { Owner = _view };
            window.ShowDialog();
            _view.Close();
        }

        public void AbrirViewTransacoes(string NumConta)
        {
            _view.Opacity = 0.5;
            var dialog = new ContaTransacaoDialog(int.Parse(NumConta)) { Owner = _view };
            bool? resultado = dialog.ShowDialog();
            _view.Opacity = 1;
        }
    }
}
