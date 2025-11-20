using System;

namespace Projeto_UVV_Fintech.ViewModels
{
    public class ContaViewModel
    {
        public int ClienteId { get; set; }
        public int Agencia { get; set; }
        public int NumeroConta { get; set; }
        public string Tipo { get; set; }
        public DateTime DataCriacao { get; set; }
        public double Saldo { get; set; }
        public string NomeCliente { get; set; }
    }
}
