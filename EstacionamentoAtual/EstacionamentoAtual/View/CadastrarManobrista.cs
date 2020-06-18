using EstacionamentoAtual.Model;
using MySql.Data.MySqlClient;
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
    public partial class CadastrarManobrista : Form
    {
       
        public PainelAdmin paineladmin;
        Manobrista novomanobrista;
        Manobrista alterarmanobrista;
        Conexao.Conexao conexao;
        private Int32 catchRowIndex;
        public CadastrarManobrista()
        {
            InitializeComponent();
            carregarDados();
            alterarmanobrista = new Manobrista();
        }

        public void carregarDados()
        {

            conexao = new Conexao.Conexao();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            string connectionString = conexao.getConnectionString();
            string query = "SELECT * FROM manobrista where Status = 'Ativo' or Status = 'Inativo'";

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

                            dataGridView1.Rows.Add(dataTable.Rows[i][0], dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex);
                    }
                }

            }


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            paineladmin.Visible = true;
            this.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
           

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                novomanobrista = new Manobrista();
                novomanobrista.Nome1 = txtnomemanobrista.Text;
                novomanobrista.Cpf = txtcpfmanobrista.Text;
                novomanobrista.Status = comboBoxstatus.SelectedItem.ToString();
                novomanobrista.Idlogin = Convert.ToInt32(paineladmin.menu.Loginv.Idlogin.ToString());
                novomanobrista.InserirManobrista();
                MessageBox.Show("Manobrista Cadastrado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                carregarDados();
                txtnomemanobrista.Text = "";
                txtcpfmanobrista.Text = "";
                comboBoxstatus.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar Manobrista!: " + ex);
            }
            finally
            {
            }
        }

        private void textnomemanobrista_TextChanged(object sender, EventArgs e)
        {

        }

        private void preenchercombo()
        {
            txtnomemanobrista.Text = alterarmanobrista.Nome1;
            txtcpfmanobrista.Text = alterarmanobrista.Cpf;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            catchRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                
                alterarmanobrista.Idmanobrista = Convert.ToInt32(row.Cells[0].Value.ToString());
                alterarmanobrista.Nome1 = row.Cells[1].Value.ToString();
                alterarmanobrista.Cpf = row.Cells[2].Value.ToString();
                alterarmanobrista.Status = row.Cells[3].Value.ToString();
                alterarmanobrista.Idlogin = Convert.ToInt32(paineladmin.menu.Loginv.Idlogin.ToString());
                preenchercombo();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                alterarmanobrista.Nome1 = txtnomemanobrista.Text;
                alterarmanobrista.Cpf = txtcpfmanobrista.Text;
                alterarmanobrista.AlterarManobrista();
                MessageBox.Show("Manobrista Alterado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                carregarDados();
                txtnomemanobrista.Text = "";
                txtcpfmanobrista.Text = "";
                comboBoxstatus.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Alterar Manobrista!: " + ex);
            }
            finally
            {
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                alterarmanobrista.Status = "Excluido";
                alterarmanobrista.ExcluirManobrista();
                MessageBox.Show("Manobrista Excluido!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                carregarDados();
                txtnomemanobrista.Text = "";
                txtcpfmanobrista.Text = "";
                comboBoxstatus.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Manobrista!: " + ex);
            }
            finally
            {
            }
        }

        private void comboBoxstatus_SelectedValueChanged(object sender, EventArgs e)
        {
            alterarmanobrista.Status = comboBoxstatus.SelectedItem.ToString();

        }
    }
}
