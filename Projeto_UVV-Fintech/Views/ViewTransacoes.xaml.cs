using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;


namespace Projeto_UVV_Fintech.Views
{
    public partial class ViewTransacoes : Window
    {
        public bool valorMaiorQue = false;
        public bool dataMaiorQue = true;
        decimal valor = 0;

        public ViewTransacoes()
        {
            InitializeComponent();
        }

        private void NumericTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Usa uma expressão regular para verificar se o caractere é um dígito (0-9)
            // Se quiser permitir decimais, você precisaria de uma lógica mais complexa aqui.
            //if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "[0-9]"))
            //{
            //    e.Handled = true; // Impede que o caractere seja inserido
            //}

            // Obtém o TextBox que disparou o evento
            TextBox textBox = sender as TextBox;

            // Verifica se o caractere inserido é um dígito (0-9)
            bool isDigit = Char.IsDigit(e.Text, 0);

            // Verifica se o caractere é um separador decimal (vírgula ou ponto, dependendo da cultura)
            // Usamos CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator para ser compatível com a região.
            string decimalSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

            // Para o Brasil, o separador é a vírgula. Vamos permitir explicitamente ponto e vírgula.
            bool isSeparator = e.Text == ",";

            if (isDigit)
            {
                // Aceita dígitos
                e.Handled = false;
            }
            else if (isSeparator)
            {
                // Aceita separadores APENAS se ainda não houver um no texto
                if (textBox.Text.Contains(","))
                {
                    e.Handled = true; // Rejeita se já houver um separador
                }
                else
                {
                    e.Handled = false; // Aceita se for o primeiro separador
                }
            }
            else
            {
                // Rejeita qualquer outro caractere (letras, símbolos, espaços)
                e.Handled = true;
            }
        }

        // Opcional, mas recomendado: impede que o usuário cole texto não numérico
        private void NumericTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!System.Text.RegularExpressions.Regex.IsMatch(text, "[0-9]+"))
                {
                    e.CancelCommand(); // Cancela o comando de colar
                }
            }
            else
            {
                e.CancelCommand(); // Cancela se não for texto
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ValorMaiorQue_Click(object sender, RoutedEventArgs e)
        {
            if (valorMaiorQue)
            {
                ValorMaiorQue.Content = "Menor Que";
                valorMaiorQue = false;
            }
            else
            {
                ValorMaiorQue.Content = "Maior Que";
                valorMaiorQue = true;
            }
        }

        private void DataMaiorQue_Click(object sender, RoutedEventArgs e)
        {
            if (dataMaiorQue)
            {
                DataMaiorQue.Content = "Menor Que";
                dataMaiorQue = false;
            }
            else
            {
                DataMaiorQue.Content = "Maior Que";
                dataMaiorQue = true;
            }
        }

        private void NumericTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NumericTextBox.Text = "";
        }

        private void NumericTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(NumericTextBox.Text, out valor))
            {
                valor = decimal.Parse(NumericTextBox.Text);
            }
            else
            {
                Console.WriteLine("Erro ao converter para decimal!!!");
            }

            // Formata o valor como moeda brasileira (R$)
            string valorFormatado = String.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valor);

            // Exibe o resultado: R$ 123.456,78
            Console.WriteLine(valorFormatado);
            NumericTextBox.Text = valorFormatado;
        }
    }
}
