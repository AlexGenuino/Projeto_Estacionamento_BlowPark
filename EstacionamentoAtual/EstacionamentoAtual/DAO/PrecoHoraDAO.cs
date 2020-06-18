using EstacionamentoAtual.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace EstacionamentoAtual.DAO
{
    class PrecoHoraDAO
    {
        private MySqlConnection con;
        private Conexao.Conexao conexao;
        private List<PrecoHora> precohora;
        private Model.PrecoHora precohoradiasemana;
        public PrecoHoraDAO()
        {

        }
        internal List<PrecoHora> PrecoHora { get => precohora; set => precohora = value; }
        public List<PrecoHora> ListarPrecos(Model.PrecoHora PrecoHora)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "SELECT DiaSemana, ValorPrimeiraHora, ValorDemaisHoras, Id_DiaSemana, PrecoDiaria FROM precohora";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MySqlDataReader mysqlDT = cmd.ExecuteReader();
                precohora = new List<PrecoHora>();
                while (mysqlDT.Read())
                {
                    PrecoHora prechora1 = new PrecoHora
                    {
                        DiaSemana1 = mysqlDT.GetString("DiaSemana"),
                        Id_DiaSemana1 = Convert.ToInt32(mysqlDT.GetString("Id_DiaSemana")),
                        ValorPrimeiraHora1 = Convert.ToDouble(mysqlDT.GetString("ValorPrimeiraHora")),
                        ValorDemaisHoras1 = Convert.ToDouble(mysqlDT.GetString("ValorDemaisHoras")),
                        ValorDiaria1 = Convert.ToDouble(mysqlDT.GetString("PrecoDiaria")),
                    };
                    precohora.Add(prechora1);
                }
                return precohora;
            }
            finally
            {
                con.Close();
            }
        }

        public PrecoHora ProcurarDiaDaSemana(Model.PrecoHora PrecoHora)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "SELECT DiaSemana, ValorPrimeiraHora, ValorDemaisHoras, Id_DiaSemana, PrecoDiaria FROM precohora WHERE DiaSemana = ?DiaSemana";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?DiaSemana", PrecoHora.DiaSemana1);
                cmd.Dispose();

                MySqlDataReader mysqlDT = cmd.ExecuteReader();

                if (mysqlDT.Read())
                {

                    precohoradiasemana = new PrecoHora();
                    precohoradiasemana.DiaSemana1 = mysqlDT["DiaSemana"].ToString();
                    precohoradiasemana.ValorPrimeiraHora1 = Convert.ToDouble(mysqlDT["ValorPrimeiraHora"].ToString());
                    precohoradiasemana.ValorDemaisHoras1 = Convert.ToDouble(mysqlDT["ValorDemaisHoras"].ToString());
                    precohoradiasemana.Id_DiaSemana1 = Convert.ToInt32(mysqlDT["Id_DiaSemana"].ToString());
                    precohoradiasemana.ValorDiaria1 = Convert.ToDouble(mysqlDT["PrecoDiaria"].ToString());
                }
                else
                {
                    return null;
                    MessageBox.Show("C");
                }
                return precohoradiasemana;
            }
            finally
            {
                con.Close();
            }
        }

        public void AlterarDiaDaSemana(Model.PrecoHora PrecoHora)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "UPDATE precohora SET ValorPrimeiraHora = ?ValorPrimeiraHora, ValorDemaisHoras = ?ValorDemaisHoras, PrecoDiaria = ?PrecoDiaria  WHERE Id_DiaSemana = ?Id_DiaSemana";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?ValorPrimeiraHora", PrecoHora.ValorPrimeiraHora1);
                cmd.Parameters.AddWithValue("?ValorDemaisHoras", PrecoHora.ValorDemaisHoras1);
                cmd.Parameters.AddWithValue("?PrecoDiaria", PrecoHora.ValorDiaria1);
                cmd.Parameters.AddWithValue("?Id_DiaSemana", PrecoHora.Id_DiaSemana1);
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

        public void InserirDiaDaSemana(Model.PrecoHora PrecoHora)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "INSERT INTO precohora(DiaSemana, ValorPrimeiraHora, ValorDemaisHoras, PrecoDiaria)";
            query += " VALUES (?DiaSemana, ?ValorPrimeiraHora, ?ValorDemaisHoras, ?PrecoDiaria)";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?DiaSemana", PrecoHora.DiaSemana1);
                cmd.Parameters.AddWithValue("?ValorPrimeiraHora", PrecoHora.ValorPrimeiraHora1);
                cmd.Parameters.AddWithValue("?ValorDemaisHoras", PrecoHora.ValorDemaisHoras1);
                cmd.Parameters.AddWithValue("?PrecoDiaria", PrecoHora.ValorDiaria1);
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
