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
    class VeiculoDAO
    {
        private Conexao.Conexao conexao;
        private MySqlConnection con;
        private TipoVeiculo procurarveiculo;

        private List<TipoVeiculo> veiculos;



        public VeiculoDAO()
        {

        }

        internal List<TipoVeiculo> Veiculo { get => veiculos; set => veiculos = value; }

        public void InserirVeiculo(Model.TipoVeiculo Veiculo)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "INSERT INTO veiculo(Modelo, Placa, Tipo, CPF, Marca)";
            query += " VALUES (?Modelo, ?Placa, ?Tipo, ?CPF, ?Marca)";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Modelo", Veiculo.Modelo1);
                cmd.Parameters.AddWithValue("?Placa", Veiculo.Placa1);
                cmd.Parameters.AddWithValue("?Tipo", Veiculo.Tipo1);
                cmd.Parameters.AddWithValue("?CPF", Veiculo.Cpfveiculo);
                cmd.Parameters.AddWithValue("?Marca", Veiculo.Marca1);
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

        public List<TipoVeiculo> ListarVeiculos(Model.TipoVeiculo Veiculo)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "SELECT Modelo, Id_Veiculo, Placa, Tipo, Marca, CPF FROM veiculo WHERE CPF = ?CPF";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?CPF", Veiculo.Cpfveiculo);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                MySqlDataReader mysqlDT = cmd.ExecuteReader();
                veiculos = new List<TipoVeiculo>();
                while (mysqlDT.Read())
                {
                    TipoVeiculo veiculo1 = new TipoVeiculo
                    {
                        Modelo1 = mysqlDT.GetString("Modelo"),
                        Idveiculo = Convert.ToInt32(mysqlDT.GetString("Id_Veiculo")),
                        Placa1 = mysqlDT.GetString("Placa"),
                        Tipo1 = mysqlDT.GetString("Tipo"),
                        Marca1 = mysqlDT.GetString("Marca"),
                        Cpfveiculo = mysqlDT.GetString("CPF"),

                    };
                    veiculos.Add(veiculo1);
                }
                return veiculos;
            }
            finally
            {
                con.Close();
            }
        }

        public List<TipoVeiculo> ListarVeiculosEstacionados()
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "SELECT a.Id_Entrada, a.Data_Hora, a.IdManobrista, a.Id_Veiculo, c.Placa, c.Modelo, c.Marca, c.CPF, d.Nome, a.Id_Vaga, b.NumeroVaga, b.Status, f.Data_Hora, c.Tipo from entrada a INNER JOIN vaga b ON b.Id_Vaga = a.Id_Vaga INNER JOIN veiculo c ON c.Id_Veiculo = a.Id_Veiculo INNER JOIN cliente d on d.CPF = c.CPF LEFT JOIN saida f on f.Id_Entrada = a.Id_Entrada where b.Status = 'Ocupado' and f.Data_Hora is null";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                MySqlDataReader mysqlDT = cmd.ExecuteReader();
                veiculos = new List<TipoVeiculo>();
                while (mysqlDT.Read())
                {
                    TipoVeiculo veiculo1 = new TipoVeiculo
                    {
                        Modelo1 = mysqlDT.GetString("Modelo"),
                    };
                    veiculos.Add(veiculo1);
                }
                return veiculos;
            }
            finally
            {
                con.Close();
            }
        }

        public TipoVeiculo ProcurarVeiculo(Model.TipoVeiculo veiculo)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "SELECT CPF, Placa, Id_Veiculo, Modelo, Marca, Tipo FROM veiculo WHERE CPF = ?CPF and Placa = ?Placa";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?CPF", veiculo.Cpfveiculo);
                cmd.Parameters.AddWithValue("?Placa", veiculo.Placa1);
                cmd.Dispose();
                MySqlDataReader mysqlDT = cmd.ExecuteReader();
                if (mysqlDT.Read())
                {

                    procurarveiculo = new TipoVeiculo();
                    procurarveiculo.Modelo1 = mysqlDT["Modelo"].ToString();
                    procurarveiculo.Idveiculo = Convert.ToInt32(mysqlDT["Id_Veiculo"].ToString());
                    procurarveiculo.Placa1 = mysqlDT["Placa"].ToString();
                    procurarveiculo.Marca1 = mysqlDT["Marca"].ToString();
                    procurarveiculo.Tipo1 = mysqlDT["Tipo"].ToString();
                    procurarveiculo.Tipo1 = mysqlDT["CPF"].ToString();
                }
                else
                {
                    return null;
                }
                return procurarveiculo;
            }
            finally
            {
                con.Close();
            }
        }

        public void AlterarVeiculo(Model.TipoVeiculo veiculo)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "UPDATE veiculo SET Modelo = ?Modelo, Placa = ?Placa, Marca = ?Marca, Tipo = ?Tipo WHERE Id_Veiculo = ?Id_Veiculo";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Modelo", veiculo.Modelo1);
                cmd.Parameters.AddWithValue("?Placa", veiculo.Placa1);
                cmd.Parameters.AddWithValue("?Marca", veiculo.Marca1);
                cmd.Parameters.AddWithValue("?Tipo", veiculo.Tipo1);
                cmd.Parameters.AddWithValue("?Id_Veiculo", veiculo.Idveiculo);
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
