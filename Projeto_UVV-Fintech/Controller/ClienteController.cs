//using Projeto_UVV_Fintech.Model;
using Projeto_UVV_Fintech.Banco_Dados.Entities;
using Projeto_UVV_Fintech.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projeto_UVV_Fintech.Controller
{
    public class ClienteController
    {
        private readonly ViewClientes _view;

        public ClienteController(ViewClientes view)
        {
            _view = view;
        }

        //Comentários para evitar erros de compilação pela falta dos métodos em model/Conta.cs
        public bool CriarCliente()
        {
            _view.Opacity = 0.5;

            var dialog = new ClienteDialog { Owner = _view };
            bool? resultado = dialog.ShowDialog();

            _view.Opacity = 1;

            if (resultado == true)
            {
                string nome = dialog.NomeCliente;
                string telefone = dialog.TelefoneCliente;
                string cep = dialog.CepCliente;

                //if (Cliente.AdicionarConta(nome, telefone, cep))
                //{
                //    MessageBox.Show($"Cliente salvo:\nNome: {nome}\nTelefone: {telefone}\nCEP: {cep}");
                //    return true;
                //}
            }

            return false;
        }

        public List<Cliente> ListarClientes()
        {
            //List<Cliente> resultado = Cliente.ListarClientes();
            //_view.TabelaClientes.ItemsSource = resultado;
            //return resultado;
            return new List<Cliente>();
        }

        public void FiltrarClientes(int? idCliente, string? telefone, string? cep, string? nomeCliente, int? numeroDeContas, DateTime? dataAdesao, bool? dataMaiorQue)
        {
            //List<Cliente> resultado = Cliente.FiltrarContas(
            //    idCliente, telefone, cep, nomeCliente,
            //    numeroDeContas, dataAdesao, dataMaiorQue);

            //_view.TabelaClientes.ItemsSource = resultado;
        }

        public void AbrirViewContas(Cliente clienteSelecionado)
        {
            _view.Hide();
            var window = new ViewContas(clienteSelecionado) { Owner = _view };
            window.ShowDialog();
            _view.Close();
        }

        //public static Cliente? ObterContaPorId(int idCliente)
        //{
        //    return Cliente.ObterClientePorId(idCliente);
        //}

    }
}
