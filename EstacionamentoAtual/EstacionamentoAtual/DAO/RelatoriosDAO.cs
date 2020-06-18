using EstacionamentoAtual.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.DAO
{
    class RelatoriosDAO
    {


        private List<Model.RelatorioPagamentos> listrelatoriovalores;
        private MySqlConnection con;
        private Conexao.Conexao conexao;

        public RelatoriosDAO()
        {

        }

        internal List<RelatorioPagamentos> Listrelatoriovalores { get => listrelatoriovalores; set => listrelatoriovalores = value; }

        public List<Model.RelatorioPagamentos> RetornarValoresRelatorio(Model.RelatorioPagamentos data)
        {
            con = new MySqlConnection();
            conexao = new Conexao.Conexao();
            con.ConnectionString = conexao.getConnectionString();
            String query = "SELECT d.NumeroVaga, a.Cliente, a.Placa, c.Data_Hora horaentrada, b.Data_Hora horasaida, b.Tempo_Permanecido, a.Valor, a.TipoPagamento from pagamento a inner join saida b on b.Id_Saida = a.Id_Saida inner join entrada c on c.Id_Entrada = b.Id_Entrada inner join vaga d on d.Id_Vaga = c.Id_Vaga where c.Data_Hora between ?DataInicio and ?DataFinal";
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("?DataInicio", data.Datainicial.Date);
                cmd.Parameters.AddWithValue("?DataFinal", data.Datafinal.Date);
                listrelatoriovalores = new List<Model.RelatorioPagamentos>();
                MySqlDataReader mysqlDT = cmd.ExecuteReader();
                while (mysqlDT.Read())
                {
                    Model.RelatorioPagamentos valores = new Model.RelatorioPagamentos
                    {
                        Numerovaga = Convert.ToInt32(mysqlDT.GetString("NumeroVaga")),
                        Cliente = mysqlDT.GetString("Cliente"),
                        Placa = mysqlDT.GetString("Placa"),
                        Data_Entrada1 = Convert.ToDateTime(mysqlDT.GetString("horaentrada")),
                        Data_Saida1 = Convert.ToDateTime(mysqlDT.GetString("horasaida")),
                        Tempo_Permanecido1 = mysqlDT.GetString("Tempo_Permanecido"),
                        Valor = Convert.ToDouble(mysqlDT.GetString("Valor")),
                        Tipo_Pagamento1 = mysqlDT.GetString("TipoPagamento"),
                    };
                    listrelatoriovalores.Add(valores);
                }
                return listrelatoriovalores;
            }
            finally
            {
                con.Close();
            }
        }


    }
}
