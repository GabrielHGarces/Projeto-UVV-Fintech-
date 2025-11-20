using Microsoft.EntityFrameworkCore;
using Projeto_UVV_Fintech.Banco_Dados.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.Repository
{
    internal class ContaPoupancaRepository
    {
        public static bool CriarConta(int clienteId)
        {
            using var context = new DB_Context();
            Conta novo = new ContaPoupanca();
            var clienteAssociado = context.Clientes.Find(clienteId);
            novo.Saldo = 0;
            novo.ClienteId = clienteId;
            novo.Cliente = clienteAssociado;
            Random rand = new Random();
            novo.Agencia = rand.Next(10000, 99999); // Gera um número de agência aleatório entre 10000 e 99999
            novo.NumeroConta = rand.Next(100000, 999999); // Gera um número de conta aleatório entre 100000 e 999999

            clienteAssociado.NumeroContasCliente += 1;
            clienteAssociado.Contas.Add(novo);
            context.Contas.Add(novo);
            context.SaveChanges();

            return true;
        }

        public static List<ContaPoupanca> ListarContas()
        {
            using var context = new DB_Context();
            return context.Contas.OfType<ContaPoupanca>().Include(c => c.Cliente).ToList();
        }

        public static bool Depositar(Conta conta, double valor)
        {
            if (conta == null)
            {
                return false;
            }



            conta.Saldo += valor;
            using var context = new DB_Context();
            context.Contas.Update(conta);

            context.SaveChanges();
            TransacaoRepository.CriarTransacao(TipoTransacao.Deposito, valor, conta.Id, conta.Id, conta.Id);
            return true;




        }

        public static bool  Sacar(Conta conta, double valor)
        {
            if (conta == null || conta.Saldo < valor)
            {
                return false;
            }



            conta.Saldo -= valor;
            using var context = new DB_Context();
            context.Contas.Update(conta);
            context.SaveChanges();
            TransacaoRepository.CriarTransacao(TipoTransacao.Deposito, valor, conta.Id, conta.Id, conta.Id);
            return true;

        }

        public static bool Transferir(Conta contaOrigem, Conta contaDestino, double valor)
        {
            if (contaOrigem == null || contaDestino == null)
                return false;




            if (contaOrigem.Saldo < valor)
            {

                return false;
            }
            else
            {
                using var context = new DB_Context();

                contaOrigem.Saldo -= valor;
                contaDestino.Saldo += valor;

                context.Contas.Update(contaOrigem);
                context.Contas.Update(contaDestino);


                context.SaveChanges();
                TransacaoRepository.CriarTransacao(TipoTransacao.Deposito, valor, contaOrigem.Id, contaDestino.Id, contaOrigem.Id);
                return true;
            }


        }


        public static List<Conta> FiltrarContas(int? idCliente, int? numeroConta, int? numeroAgencia, string? tipoConta, string? nomeTitular, double? saldo, DateTime? dataCriacao, bool? saldoMaior, bool? dataMaior)
        {
            using var context = new DB_Context();
            IQueryable<Conta> query = context.Contas.OfType<ContaPoupanca>().Include(c => c.Cliente);

            if (idCliente.HasValue)
                query = query.Where(c => c.ClienteId == idCliente.Value);

            if (numeroConta.HasValue)
                query = query.Where(c => c.NumeroConta == numeroConta.Value);

            if (numeroAgencia.HasValue)
                query = query.Where(c => c.Agencia == numeroAgencia.Value);

            if (!string.IsNullOrWhiteSpace(nomeTitular))
                query = query.Where(c => c.Cliente.Nome.ToUpper().Contains(nomeTitular.ToUpper()));

            if (saldo.HasValue)
            {
                if (saldoMaior == true)
                    query = query.Where(c => c.Saldo >= saldo.Value);
                else
                    query = query.Where(c => c.Saldo <= saldo.Value);
            }

            if (dataCriacao.HasValue)
            {
                if (dataMaior == true)
                    query = query.Where(c => c.DataCriacao >= dataCriacao.Value);
                else
                    query = query.Where(c => c.DataCriacao <= dataCriacao.Value);
            }

            return query.ToList();
        }


        public void AtualizarContaPoupanca(int contaId, double novoSaldo)
        {
            using var context = new DB_Context();
            var conta = context.Contas.Find(contaId);
            if (conta != null && conta is ContaPoupanca)
            {
                conta.Saldo = novoSaldo;
                context.SaveChanges();
            }
            else
            {
                //MessageBox.Show("Conta Poupança não encontrada.");
            }
        }

        public void DeletarContaPoupanca(int contaId)
        {
            using var context = new DB_Context();
            var conta = context.Contas.Find(contaId);
            if (conta != null && conta is ContaPoupanca)
            {
                context.Contas.Remove(conta);
                context.SaveChanges();
            }
            else
            {
                //MessageBox.Show("Conta Poupança não encontrada.");
            }
        }

        public ContaPoupanca? ObterContaPorId(int contaId)
        {
            using var context = new DB_Context();
            var conta = context.Contas.Find(contaId);
            if (conta != null && conta is ContaPoupanca poupanca)
            {
                return poupanca;
            }
            return null;
        }

        public List<ContaPoupanca> ObterTodasContasPoupanca()
        {
            using var context = new DB_Context();
            return context.Contas.OfType<ContaPoupanca>().ToList();
        }

        public void BuscarPorId(int contaId)
        {
            using var context = new DB_Context();
            var conta = context.Contas.Find(contaId);
            if (conta != null && conta is ContaPoupanca)
            {
                //MessageBox.Show($"ID: {conta.Id}, Tipo de Conta: Poupança, Saldo: {conta.Saldo}, ClienteId: {conta.ClienteId}, Data de Criação: {conta.DataCriacao}");
            }
            else
            {
                //MessageBox.Show("Conta Poupança não encontrada.");
            }

        }

        public void BuscarPorClienteId(int clienteId)
        {
            using var context = new DB_Context();
            var contas = context.Contas.OfType<ContaPoupanca>().Where(c => c.ClienteId == clienteId).ToList();
            if (contas.Any())
            {
                foreach (var conta in contas)
                {
                    //MessageBox.Show($"ID: {conta.Id}, Tipo de Conta: Poupança, Saldo: {conta.Saldo}, ClienteId: {conta.ClienteId}, Data de Criação: {conta.DataCriacao}");
                }
            }
            else
            {
                //MessageBox.Show("Nenhuma Conta Poupança encontrada para este Cliente.");
            }
        }

        public void BuscarPorTipoConta(int IdConta)
        {
            using var context = new DB_Context();
            var contas = context.Contas.OfType<ContaPoupanca>().ToList();
            foreach (var conta in contas)
            {
                //MessageBox.Show($"ID: {conta.Id}, Tipo de Conta: Poupança, Saldo: {conta.Saldo}, ClienteId: {conta.ClienteId}, Data de Criação: {conta.DataCriacao}");
            }
        }


        public void BuscarPorSaldoMaiorQue(double saldoMinimo)
        {
            using var context = new DB_Context();
            var contas = context.Contas.OfType<ContaPoupanca>().Where(c => c.Saldo > saldoMinimo).ToList();
            if (contas.Any())
            {
                foreach (var conta in contas)
                {
                    //MessageBox.Show($"ID: {conta.Id}, Tipo de Conta: Poupança, Saldo: {conta.Saldo}, ClienteId: {conta.ClienteId}, Data de Criação: {conta.DataCriacao}");
                }
            }
            else
            {
                //MessageBox.Show("Nenhuma Conta Poupança encontrada com saldo maior que o valor especificado.");
            }
        }

        public void BuscarPorSaldoMenorQue(double saldoMaximo)
        {
            using var context = new DB_Context();
            var contas = context.Contas.OfType<ContaPoupanca>().Where(c => c.Saldo < saldoMaximo).ToList();
            if (contas.Any())
            {
                foreach (var conta in contas)
                {
                    //MessageBox.Show($"ID: {conta.Id}, Tipo de Conta: Poupança, Saldo: {conta.Saldo}, ClienteId: {conta.ClienteId}, Data de Criação: {conta.DataCriacao}");
                }
            }
            else
            {
                //MessageBox.Show("Nenhuma Conta Poupança encontrada com saldo menor que o valor especificado.");
            }
        }

        public void BuscarPorNomeCliente(string nomeCliente)
        {
            using var context = new DB_Context();
            var contas = context.Contas.OfType<ContaPoupanca>()
                .Where(c => c.Cliente.Nome.Contains(nomeCliente))
                .ToList();
            if (contas.Any())
            {
                foreach (var conta in contas)
                {
                    //MessageBox.Show($"ID: {conta.Id}, Tipo de Conta: Poupança, Saldo: {conta.Saldo}, ClienteId: {conta.ClienteId}, Data de Criação: {conta.DataCriacao}");
                }
            }
            else
            {
                //MessageBox.Show("Nenhuma Conta Poupança encontrada para o nome de cliente especificado.");
            }
        }
    }
}
