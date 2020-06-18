using EstacionamentoAtual.Model;
using EstacionamentoAtual.View;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstacionamentoAtual.DAO
{
    public class LoginDAO
    {
        private Model.Usuario novologin;
        private MySqlConnection con;
        private Conexao.Conexao conexao;
        private Menu menu;

        internal Usuario Novologin { get => novologin; set => novologin = value; }

        public void VerificaLogin(Model.Usuario usuario)
        {
            con = new MySqlConnection();
            novologin = new Model.Usuario();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "SELECT Login, Permissao, Senha, IdLogin, Status FROM login WHERE Login = ?Login and Senha = ?Senha";

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Login", usuario.Login1);
                cmd.Parameters.AddWithValue("?Senha", usuario.Senha1);
                cmd.Parameters.AddWithValue("?Permissao", usuario.Permissao1);
                cmd.Parameters.AddWithValue("?IdLogin", usuario.Idlogin);
                cmd.Parameters.AddWithValue("?Status", usuario.Status);
                cmd.Dispose();

                MySqlDataReader mysqlDT = cmd.ExecuteReader();

                if (mysqlDT.Read())
                {
                    string Permissao = mysqlDT["Permissao"].ToString();
                    string Login = mysqlDT["Login"].ToString();
                    string Senha = mysqlDT["Senha"].ToString();
                    string IdLogin = mysqlDT["IdLogin"].ToString();
                    string Status = mysqlDT["Status"].ToString();

                    if (Permissao.Equals("Admin") && Status.Equals("Ativo"))
                    {
                        novologin.Login1 = Login.ToString();
                        novologin.Senha1 = Senha.ToString();
                        novologin.Permissao1 = Permissao.ToString();
                        novologin.Idlogin = Convert.ToInt32(IdLogin.ToString());
                        novologin.Status = Status.ToString();
                        menu = new Menu(this);
                        menu.ShowDialog();
                    }
                    else if  (Permissao.Equals("Funcionario") && Status.Equals("Ativo"))
                    {
                        novologin.Login1 = Login.ToString();
                        novologin.Senha1 = Senha.ToString();
                        novologin.Permissao1 = Permissao.ToString();
                        novologin.Idlogin = Convert.ToInt32(IdLogin.ToString());
                        menu = new Menu(this);
                        menu.ShowDialog();
                    }

                }

                else
                {
                    MessageBox.Show("Usuario ou senha incorreto");
                    View.Login tentarnovamente = new View.Login();
                    tentarnovamente.ShowDialog();
                }
            }
            finally
            {
                con.Close();
            }
        }
    
        public void InserirLogin(Model.Usuario Usuario)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "INSERT INTO login(Login, Senha, Permissao, Status)";
            query += " VALUES (?Login, ?Senha, ?Permissao, ?Status)";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Login", Usuario.Login1);
                cmd.Parameters.AddWithValue("?Senha", Usuario.Senha1);
                cmd.Parameters.AddWithValue("?Permissao", Usuario.Permissao1);
                cmd.Parameters.AddWithValue("?Status", Usuario.Status);
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
        public void AlterarLogin(Model.Usuario Usuario)
            {
                con = new MySqlConnection();
                conexao = new Conexao.Conexao();
                con.ConnectionString = conexao.getConnectionString();
                String query = "UPDATE login SET Login = ?Login, Senha = ?Senha, Status = ?Status, Permissao = ?Permissao WHERE IdLogin = ?IdLogin";
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("?Login", Usuario.Login1);
                    cmd.Parameters.AddWithValue("?Senha", Usuario.Senha1);
                    cmd.Parameters.AddWithValue("?Status", Usuario.Status);
                    cmd.Parameters.AddWithValue("?Permissao", Usuario.Permissao1);
                    cmd.Parameters.AddWithValue("?IdLogin", Usuario.Idlogin);
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
            public void ExcluirLogin(Model.Usuario usuario)
            {
                con = new MySqlConnection();
                conexao = new Conexao.Conexao();
                con.ConnectionString = conexao.getConnectionString();
                String query = "UPDATE login SET Status = ?Status WHERE IdLogin = ?IdLogin"; ;
                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("?Status", usuario.Status);
                    cmd.Parameters.AddWithValue("?IdLogin", usuario.Idlogin);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }