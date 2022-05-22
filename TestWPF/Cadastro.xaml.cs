using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace TestWPF
{
    /// <summary>
    /// Lógica interna para Window1.xaml
    /// </summary>
    public partial class Cadastro : Window
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            string connString = "SERVER=localhost;DATABASE=test;UID=root;PASSWORD=";

            MySqlConnection connection = new MySqlConnection(connString);

            MySqlCommand cmd = new MySqlCommand("insert into tabletest (Nome, Sobrenome, Telefone) values ('"+Nome.Text+ "', '" + Sobrenome.Text + "', '" + Telefone.Text + "')", connection);
            connection.Open();
            cmd.ExecuteReader();
            connection.Close();

            Close();
        }
    }
}
