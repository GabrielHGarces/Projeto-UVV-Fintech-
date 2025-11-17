using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_UVV_Fintech.Model
{
    internal class Cliente
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        
       
        public DateTime DataNascimento { get; set; }
        public string CEP { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public ICollection<Conta> Contas { get; set; } = [];


        public Cliente()
        {

        }

        public Cliente( string name, DateTime dataNascimento, string cEP, string telefone, ICollection<Conta> contas)
        {
            
            Name = name;
            DataNascimento = dataNascimento;
            CEP = cEP;
            Telefone = telefone;
            Contas = contas;
        }

        //Liga uma conta a um cliente

    }
    
}
