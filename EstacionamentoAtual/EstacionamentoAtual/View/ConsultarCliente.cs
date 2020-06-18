using EstacionamentoAtual.Model;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstacionamentoAtual.View
{
    public partial class ConsultarCliente : Form
    {
        private Conexao.Conexao conexao;
        public Menu menu;
        private Int32 catchRowIndex;
        TipoVeiculo alterarveiculo;
        Cliente alterarcliente;
        Cliente verificarcliente;
        Usuario mostraruser;

        public ConsultarCliente()
        {
            InitializeComponent();
            alterarveiculo = new Model.TipoVeiculo();
            alterarcliente = new Model.Cliente();
            verificarcliente = new Model.Cliente();
            mostraruser = new Model.Usuario();

            carregarDados();
        }

        public void carregarDados1(string nomeCliente)
        {
            conexao = new Conexao.Conexao();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            string connectionString = conexao.getConnectionString();
            string query = "SELECT a.Nome, a.CPF, a.Status, a.IdLogin, c.Login, b.Id_Veiculo, b.Modelo, b.Placa, b.Marca, b.Tipo from cliente a INNER JOIN veiculo b ON b.CPF = a.CPF INNER JOIN login c ON c.IdLogin = a.IdLogin where a.Status = 'Ativo' and a.Nome like '%" + nomeCliente + "%';";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    try
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            dataGridView1.Rows.Add(dataTable.Rows[i][0], dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3], dataTable.Rows[i][4], dataTable.Rows[i][5], dataTable.Rows[i][6], dataTable.Rows[i][7], dataTable.Rows[i][8], dataTable.Rows[i][9]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex);
                    }
                }
            }
        }

        public void carregarDados()
        {
            conexao = new Conexao.Conexao();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            string connectionString = conexao.getConnectionString();
            string query = "SELECT a.Nome, a.CPF, a.Status, a.IdLogin, c.Login, b.Id_Veiculo, b.Modelo, b.Placa, b.Marca, b.Tipo from cliente a INNER JOIN veiculo b ON b.CPF = a.CPF INNER JOIN login c ON c.IdLogin = a.IdLogin where a.Status = 'Ativo'";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    try
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            dataGridView1.Rows.Add(dataTable.Rows[i][0], dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3], dataTable.Rows[i][4], dataTable.Rows[i][5], dataTable.Rows[i][6], dataTable.Rows[i][7], dataTable.Rows[i][8], dataTable.Rows[i][9]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex);
                    }
                }
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            menu.Visible = true;
            this.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void preenchercombo()
        {
            txtnome.Text = alterarcliente.Nome1;
            txtcpf.Text = alterarcliente.CPFcliente1;
            txtmodelo.Text = alterarveiculo.Modelo1;
            txtplaca.Text = alterarveiculo.Placa1;
            txtmarca.Text = alterarveiculo.Marca1;
            txttipo.Text = alterarveiculo.Tipo1;
            txtloginnome.Text = mostraruser.Login1;
        }

        private bool ClientefoiAlterado()
        {
            verificarcliente.CPFcliente1 = txtcpf.Text;
            verificarcliente = verificarcliente.ProcurarCliente();
            if (verificarcliente.Nome1 == txtnome.Text && verificarcliente.CPFcliente1 == txtcpf.Text)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void AlterarCliente()
        {
            if (ClientefoiAlterado() == true)
            {
                try
                {
                    alterarcliente.Nome1 = txtnome.Text;
                    alterarcliente.CPFcliente1 = txtcpf.Text;
                    alterarveiculo.Modelo1 = txtmodelo.Text;
                    alterarveiculo.Placa1 = txtplaca.Text;
                    alterarveiculo.Marca1 = txtmarca.Text;
                    alterarveiculo.Tipo1 = txttipo.Text;
                    alterarcliente.IdLogin = menu.Loginv.Idlogin;
                    alterarcliente.AtualizarCliente();
                    alterarveiculo.AlterarVeiculo();
                    MessageBox.Show("Alteração Concluida");
                    carregarDados();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex);
                }
            }
            else
            {
                try
                {
                    alterarcliente.Nome1 = txtnome.Text;
                    alterarcliente.CPFcliente1 = txtcpf.Text;
                    alterarveiculo.Modelo1 = txtmodelo.Text;
                    alterarveiculo.Placa1 = txtplaca.Text;
                    alterarveiculo.Marca1 = txtmarca.Text;
                    alterarveiculo.Tipo1 = txttipo.Text;
                    alterarcliente.IdLogin = menu.Loginv.Idlogin;
                    alterarveiculo.AlterarVeiculo();
                    MessageBox.Show("Alteração Concluida");
                    carregarDados();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex);
                }
            }

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            catchRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                alterarcliente.Nome1 = row.Cells[0].Value.ToString();
                alterarcliente.CPFcliente1 = row.Cells[1].Value.ToString();
                alterarcliente.Status = row.Cells[2].Value.ToString();
                alterarcliente.IdLogin = Convert.ToInt32(row.Cells[3].Value.ToString());
                mostraruser.Login1 = row.Cells[4].Value.ToString();
                alterarveiculo.Idveiculo = Convert.ToInt32(row.Cells[5].Value.ToString());
                alterarveiculo.Modelo1 = row.Cells[6].Value.ToString();
                alterarveiculo.Placa1 = row.Cells[7].Value.ToString();
                alterarveiculo.Marca1 = row.Cells[8].Value.ToString();
                alterarveiculo.Tipo1 = row.Cells[9].Value.ToString();
                preenchercombo();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            menu.Visible = true;
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlterarCliente();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            carregarDados1(textBoxcliente.Text);
        }
    }
}
