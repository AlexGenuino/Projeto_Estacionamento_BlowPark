using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstacionamentoAtual.DAO
{
    class SaidaVagaDAO
    {
        private MySqlConnection con;
        private Conexao.Conexao conexao;

        public void InserirSaida(Model.SaidaVaga NovaSaida)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "INSERT INTO saida(Data_Hora, IdManobrista, Id_Entrada, Tempo_Permanecido)";
            query += " VALUES (?Data_Hora, ?IdManobrista, ?Id_Entrada, ?Tempo_Permanecido);SELECT LAST_INSERT_ID() as id;";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?Data_Hora", NovaSaida.Datahorasaida);
                cmd.Parameters.AddWithValue("?IdManobrista", NovaSaida.Idmanobrista);
                cmd.Parameters.AddWithValue("?Id_Entrada", NovaSaida.Identrada);
                cmd.Parameters.AddWithValue("?Tempo_Permanecido", NovaSaida.Datahorasaida.Subtract(NovaSaida.Datahoraentrada));
                MySqlDataReader reader = cmd.ExecuteReader();
                var idvaga = 0;
                while (reader.Read())
                {
                    idvaga = reader.GetInt32("id");
                }
                cmd.Dispose();
                reader.Close();
                String query2 = "INSERT INTO pagamento(Cliente, Valor, Placa, TipoPagamento, Id_Saida)" +
                        " VALUES (?Cliente, ?Valor, ?Placa, ?TipoPagamento, ?Id_Saida);";
                MySqlCommand cmd2 = new MySqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("?Cliente", NovaSaida.Cliente1);
                cmd2.Parameters.AddWithValue("?Valor", NovaSaida.Valor);
                cmd2.Parameters.AddWithValue("?Placa", NovaSaida.Placa1);
                cmd2.Parameters.AddWithValue("?TipoPagamento", NovaSaida.TipoPagamento1);
                cmd2.Parameters.AddWithValue("?Id_Saida", idvaga);
                cmd2.ExecuteNonQuery();
                cmd2.Dispose();
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

