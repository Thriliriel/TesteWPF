using GalaSoft.MvvmLight.Command;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Lógica interna para Listar.xaml
    /// </summary>
    public partial class Listar : Window
    {
        public Listar()
        {
            InitializeComponent();

            string connString = "SERVER=localhost;DATABASE=test;UID=root;PASSWORD=";

            MySqlConnection connection = new MySqlConnection(connString);

            //MySqlCommand cmd = new MySqlCommand("select * from tabletest", connection);
            connection.Open();
            DataTable dt = new DataTable();
            //dt.Columns.Add("Excluir", typeof(Button));
            //dt.Columns.Add("Editar", typeof(Button));
            dt.Columns.Add("id");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Sobrenome");
            dt.Columns.Add("Telefone");

            using (MySqlCommand command = new MySqlCommand("select * from tabletest", connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Id"].ToString() != "")
                        {

                            string id = reader["Id"].ToString();
                            string name = reader["Nome"].ToString();
                            string surname = reader["Sobrenome"].ToString();
                            string phone = reader["Telefone"].ToString();

                            DataRow newRow = dt.NewRow();
                            //newRow["Excluir"] = new Button()
                            //{
                            //    Name = "rowButton",
                            //    Content = "Row Button Content",
                            //    Width = 100,
                            //    Height = 30
                            //};
                            //newRow["Editar"] = new Button
                            //{
                            //    Name = "rowButton",
                            //    Content = "Row Button Content",
                            //    Width = 100,
                            //    Height = 30
                            //};
                            newRow["id"] = id;
                            newRow["Nome"] = name;
                            newRow["Sobrenome"] = surname;
                            newRow["Telefone"] = phone;

                            dt.Rows.Add(newRow);
                        }
                    }
                }
            }

            connection.Close();

            dtGrid.DataContext = dt;

        }

        private void Deletar(object sender, RoutedEventArgs e)
        {
            string connString = "SERVER=localhost;DATABASE=test;UID=root;PASSWORD=";

            MySqlConnection connection = new MySqlConnection(connString);

            DataRowView drv = (DataRowView)((Button)sender).Tag;

            MySqlCommand cmd = new MySqlCommand("delete from tabletest where Id = " + drv.Row.ItemArray[0], connection);
            connection.Open();
            cmd.ExecuteReader();
            connection.Close();
            drv.Delete();
        }

        private void Editar(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)((Button)sender).Tag;

            CadastroEdicao ce = new CadastroEdicao();
            ce.Id.Text = drv.Row.ItemArray[0].ToString();
            ce.Nome.Text = drv.Row.ItemArray[1].ToString();
            ce.Sobrenome.Text = drv.Row.ItemArray[2].ToString();
            ce.Telefone.Text = drv.Row.ItemArray[3].ToString();
            ce.Show();
        }
    }
}
