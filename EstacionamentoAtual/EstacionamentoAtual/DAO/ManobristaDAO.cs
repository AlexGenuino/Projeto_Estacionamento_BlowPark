using EstacionamentoAtual.Model;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstacionamentoAtual.DAO
{
    class ManobristaDAO
    {
        private Conexao.Conexao conexao;
        private MySqlConnection con;
        private List<Manobrista> manobrista;

        internal List<Manobrista> Manobrista { get => manobrista; set => manobrista = value; }

        public ManobristaDAO()
        {

        }
        public void InserirManobrista(Model.Manobrista Manobrista)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "INSERT INTO manobrista(Nome, Status, IdLogin, CPF)";
            query += " VALUES (?Nome, ?Status, ?IdLogin, ?CPF)";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Nome", Manobrista.Nome1);
                cmd.Parameters.AddWithValue("?Status", Manobrista.Status);
                cmd.Parameters.AddWithValue("?CPF", Manobrista.Cpf);
                cmd.Parameters.AddWithValue("?IdLogin", Manobrista.Idlogin);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }
            finally
            {
                con.Close();
            }
        }

        public void AlterarManobrista(Model.Manobrista Manobrista)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "UPDATE manobrista SET Nome = ?Nome, CPF = ?CPF, Status = ?Status WHERE IdManobrista = ?IdManobrista";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Nome", Manobrista.Nome1);
                cmd.Parameters.AddWithValue("?CPF", Manobrista.Cpf);
                cmd.Parameters.AddWithValue("?IdManobrista", Manobrista.Idmanobrista);
                cmd.Parameters.AddWithValue("?Status", Manobrista.Status);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex);
            }
            finally
            {
                con.Close();
            }

        }
        public void ExcluirManobrista(Model.Manobrista Manobrista)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "UPDATE manobrista SET Status = ?Status WHERE IdManobrista = ?IdManobrista"; ;
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Status", Manobrista.Status);
                cmd.Parameters.AddWithValue("?IdManobrista", Manobrista.Idmanobrista);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            finally
            {
                con.Close();
            }
        }

        public List<Manobrista>ListarManobrista(Model.Manobrista manobrista)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "SELECT IdManobrista, Nome, CPF, Status, IdLogin FROM manobrista WHERE Status = 'Ativo'";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Dispose();
                MySqlDataReader mysqlDT = cmd.ExecuteReader();
                Manobrista = new List<Manobrista>();
                while (mysqlDT.Read())
                {
                    Manobrista manobrista1 = new Manobrista
                    {
                        Cpf = mysqlDT.GetString("CPF"),
                        Idmanobrista = Convert.ToInt32(mysqlDT.GetString("IdManobrista")),
                        Nome1 = mysqlDT.GetString("Nome"),
                        Status = mysqlDT.GetString("Status"),
                    };
                    Manobrista.Add(manobrista1);
                }    
                return Manobrista;
            }
            finally
            {
                con.Close();
            }
        }
    }
}

 





 
