using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleConnectionMySQL
{
    public class DBConnection
    {
        MySqlConnection cnConnection;
        public DBConnection()
        {
            cnConnection = new MySqlConnection();
            cnConnection.ConnectionString = "server=localhost;uid=root;pwd=aluno;database=banco";
            try
            {
                cnConnection.Open();
            }
            catch (MySqlException me)
            {
                MessageBox.Show("Erro número " + me.Number + ": " + me.Message);
            }
            finally
            {
                cnConnection.Close();
            }
        }

        public bool insert(string nome, string login, string senha)
        {
            int inLinhasAfetadas = -1;

            try
            {
                cnConnection.Open();

                MySqlCommand cmCommand = new MySqlCommand();
                cmCommand.Connection = cnConnection;

                cmCommand.CommandText = @"INSERT INTO adUsuario (stNome, stLogin, stSenha) VALUES " +
                                        "(@nome, @login, @senha)";

                cmCommand.Parameters.AddWithValue("@nome", nome);
                cmCommand.Parameters.AddWithValue("@login", login);
                cmCommand.Parameters.AddWithValue("@senha", senha);

                inLinhasAfetadas = cmCommand.ExecuteNonQuery();
            }
            finally
            {
                cnConnection.Close();
            }

            return inLinhasAfetadas > 0;
        }

        public bool update(int cod, string nome, string login, string senha)
        {
            int inLinhasAfetadas = -1;

            try
            {
                cnConnection.Open();

                MySqlCommand cmCommand = new MySqlCommand();
                cmCommand.Connection = cnConnection;

                cmCommand.CommandText = @"UPDATE adUsuario SET stNome = @nome,"+
                                                              "stLogin = @login,"+
                                                              "stSenha = @senha"+
                                        " WHERE idUsuario = @cod";
                
                cmCommand.Parameters.AddWithValue("@cod", cod);
                cmCommand.Parameters.AddWithValue("@nome", nome);
                cmCommand.Parameters.AddWithValue("@login", login);
                cmCommand.Parameters.AddWithValue("@senha", senha);

                inLinhasAfetadas = cmCommand.ExecuteNonQuery();
            }
            finally
            {
                cnConnection.Close();
            }

            return inLinhasAfetadas > 0;
        }

        public bool delete(int cod)
        {
            int inLinhasAfetadas = -1;

            try
            {
                cnConnection.Open();

                MySqlCommand cmCommand = new MySqlCommand();
                cmCommand.Connection = cnConnection;

                cmCommand.CommandText = @"DELETE FROM adUsuario WHERE idUsuario = @cod";

                cmCommand.Parameters.AddWithValue("@cod", cod);

                inLinhasAfetadas = cmCommand.ExecuteNonQuery();
            }
            finally
            {
                cnConnection.Close();
            }

            return inLinhasAfetadas > 0;
        }

        public string selectAllFormated()
        {
            string retorno = string.Empty;

            try
            {
                cnConnection.Open();

                MySqlCommand cmCommand = new MySqlCommand();
                cmCommand.Connection = cnConnection;

                cmCommand.CommandText = $"SELECT idUsuario, stNome, stLogin FROM adUsuario";

                MySqlDataReader rdReader = cmCommand.ExecuteReader();
                while (rdReader.Read())
                {
                    string idUsuario = rdReader.GetString("idUsuario");
                    string stNome = rdReader.GetString("stNome");
                    string stLogin = rdReader.GetString("stLogin");

                    retorno += string.Format(" >  {0}  |  {1}  |  {2} .\n", idUsuario, stNome, stLogin);
                }
                rdReader.Close();
            }
            finally
            {
                cnConnection.Close();
            }

            return retorno;
        }

        public string selectByCod(int cod)
        {
            string retorno = string.Empty;

            try
            {
                cnConnection.Open();

                MySqlCommand cmCommand = new MySqlCommand();
                cmCommand.Connection = cnConnection;

                cmCommand.CommandText = $"SELECT idUsuario, stNome, stLogin, stSenha FROM adUsuario WHERE idUsuario = @cod";
                cmCommand.Parameters.AddWithValue("@cod", cod);

                MySqlDataReader rdReader = cmCommand.ExecuteReader();
                while (rdReader.Read())
                {
                    string idUsuario = rdReader.GetString("idUsuario");
                    string stNome = rdReader.GetString("stNome");
                    string stLogin = rdReader.GetString("stLogin");
                    string stSenha = rdReader.GetString("stSenha");

                    retorno = string.Format("{0},{1},{2},{3}", idUsuario, stNome, stLogin, stSenha);
                }
                rdReader.Close();
            }
            finally
            {
                cnConnection.Close();                
            }

            return retorno;
        }
    }
}
