using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstacionamentoAtual.DAO
{
    class VagaDAO
    {
        private Conexao.Conexao conexao;
        private MySqlConnection con;
        public VagaDAO()
        {

        }
        public void InserirVaga(Model.Vaga Vaga)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "INSERT INTO vaga(NumeroVaga, Status, TipoVaga)";
            query += " VALUES (?NumeroVaga, ?Status, ?TipoVaga)";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?NumeroVaga", Vaga.NumeroVaga1);
                cmd.Parameters.AddWithValue("?Status", Vaga.StatusVaga1);
                cmd.Parameters.AddWithValue("?TipoVaga", Vaga.TipoVaga1);
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

        public void AlterarVaga(Model.Vaga Vaga)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "UPDATE vaga SET NumeroVaga = ?NumeroVaga, TipoVaga = ?TipoVaga  WHERE Id_Vaga = ?Id_Vaga";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?TipoVaga", Vaga.TipoVaga1);
                cmd.Parameters.AddWithValue("?Id_Vaga", Vaga.Idvaga);
                cmd.Parameters.AddWithValue("?NumeroVaga", Vaga.NumeroVaga1);
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

        public void ExcluirVaga(Model.Vaga Vaga)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "UPDATE vaga SET Status = ?Status WHERE Id_Vaga = ?Id_Vaga"; ;
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Status", Vaga.StatusVaga1);
                cmd.Parameters.AddWithValue("?Id_Vaga", Vaga.Idvaga);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            finally
            {
                con.Close();
            }
        }

        public void VagaOcupada(Model.Vaga Vaga)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "UPDATE vaga SET Status = ?Status WHERE Id_Vaga = ?Id_Vaga";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Status", Vaga.StatusVaga1);
                cmd.Parameters.AddWithValue("?Id_Vaga", Vaga.Idvaga);
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

        public void VagaLivre(Model.Vaga Vaga)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "UPDATE vaga SET Status = ?Status WHERE Id_Vaga = ?Id_Vaga";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Status", "Livre");
                cmd.Parameters.AddWithValue("?Id_Vaga", Vaga.Idvaga);
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
    }
}

    