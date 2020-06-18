using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EstacionamentoAtual.DAO
{
    class EntradaVagaDAO
    {
        private MySqlConnection con;
        private Conexao.Conexao conexao;

        public void InserirEntrada(Model.EntradaVaga NovaEntrada)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "INSERT INTO entrada(Data_Hora, IdManobrista, Id_Veiculo, Id_Vaga)";
            query += " VALUES (?Data_Hora, ?IdManobrista, ?Id_Veiculo, ?Id_Vaga)";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Data_Hora", NovaEntrada.Datahoraentrada);
                cmd.Parameters.AddWithValue("?IdManobrista", NovaEntrada.Id_manobrista);
                cmd.Parameters.AddWithValue("?Id_Veiculo", NovaEntrada.Id_veiculo);
                cmd.Parameters.AddWithValue("?Id_Vaga", NovaEntrada.Id_vaga);
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
