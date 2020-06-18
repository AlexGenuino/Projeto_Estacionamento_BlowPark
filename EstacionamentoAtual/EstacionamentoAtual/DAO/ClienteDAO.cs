using EstacionamentoAtual.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstacionamentoAtual.DAO
{
    public class ClienteDAO
    {

        private Conexao.Conexao conexao;
        private MySqlConnection con;
        public Cliente procurarcliente;
        public ClienteDAO()
        {

        }
        public void InserirCliente(Model.Cliente Cliente)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "INSERT INTO cliente(Nome, CPF, IdLogin, Status)";
            query += " VALUES (?Nome, ?CPF, ?IdLogin, ?Status)";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Nome", Cliente.Nome1);
                cmd.Parameters.AddWithValue("?CPF", Cliente.CPFcliente1);
                cmd.Parameters.AddWithValue("?IdLogin", Cliente.IdLogin);
                cmd.Parameters.AddWithValue("?Status", Cliente.Status);
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

        public Cliente ProcurarCliente(Model.Cliente cliente)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "SELECT CPF, Nome, IdLogin, Status FROM cliente WHERE CPF = ?CPF and Status = 'Ativo'";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?CPF", cliente.CPFcliente1);
                cmd.Dispose();

                MySqlDataReader mysqlDT = cmd.ExecuteReader();

                if (mysqlDT.Read())
                {
                    
                    procurarcliente = new Cliente();
                    procurarcliente.Nome1 = mysqlDT["Nome"].ToString();
                    procurarcliente.CPFcliente1 = mysqlDT["CPF"].ToString();
                    procurarcliente.IdLogin = Convert.ToInt32(mysqlDT["IdLogin"].ToString());
                }
                else
                {
                    return null;
                    MessageBox.Show("Cliente Não Cadastrado");
                }
                return procurarcliente;

            }
           
            
            finally
            {
                
                con.Close();
            }
        }

        public void AtualizarCliente(Model.Cliente cliente)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "UPDATE cliente SET Nome = ?Nome, Status = ?Status, IdLogin = ?IdLogin WHERE CPF = ?CPF";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Nome", cliente.Nome1);
                cmd.Parameters.AddWithValue("?Status", cliente.Status);
                cmd.Parameters.AddWithValue("?CPF", cliente.CPFcliente1);
                cmd.Parameters.AddWithValue("?IdLogin", cliente.IdLogin);
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
