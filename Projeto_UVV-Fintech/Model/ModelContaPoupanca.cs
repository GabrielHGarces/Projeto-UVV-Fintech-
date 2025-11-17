using Projeto_UVV_Fintech.Banco_Dados.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.Model
{
    internal class ModelContaPoupanca :ModelConta
    {
        public void InserirPoupanca(double saldo, int clienteId)
        {
            using var context = new DB_Context();
            Conta novo = new ContaPoupanca();
            novo.Saldo = saldo;
            novo.ClienteId = clienteId;

            context.Contas.Add(novo);
            context.SaveChanges();


        }
    }
}
