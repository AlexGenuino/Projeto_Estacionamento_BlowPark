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
    public partial class CadastrarVaga : Form
    {
        public View.PainelAdmin paineladmin;
        private Conexao.Conexao conexao;
        Model.Vaga alterarvaga;
        private Model.Vaga novavaga;
        private Int32 catchRowIndex;
        public CadastrarVaga()
        {
            InitializeComponent();
            carregarDados();
        }

        public void carregarDados()
        {

            conexao = new Conexao.Conexao();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            string connectionString = conexao.getConnectionString();
            string query = "SELECT * FROM vaga where Status = 'Livre' or Status = 'Ocupado'";

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            paineladmin.Visible = true;
            this.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CadastrarVagaLote cadastrarvagalote = new CadastrarVagaLote();
            cadastrarvagalote.cadastrarvaga = this;
            cadastrarvagalote.Visible = true;
        }

        private void preenchercombo()
        {
            VagatextBox.Text = alterarvaga.NumeroVaga1.ToString();
        }
        private void LimparCampos()
        {
            VagatextBox.Clear();
            TipocomboBox.SelectedIndex = -1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            catchRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                alterarvaga = new Model.Vaga();
                alterarvaga.Idvaga = Convert.ToInt32(row.Cells[0].Value.ToString());
                alterarvaga.NumeroVaga1 = Convert.ToInt32(row.Cells[1].Value.ToString());
                alterarvaga.StatusVaga1 = row.Cells[2].Value.ToString();
                alterarvaga.TipoVaga1 = row.Cells[3].Value.ToString();
                preenchercombo();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                alterarvaga.NumeroVaga1 = Convert.ToInt32(VagatextBox.Text);
                alterarvaga.TipoVaga1 = TipocomboBox.SelectedItem.ToString();
                alterarvaga.AlterarVaga();
                MessageBox.Show("Vaga Alterada!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                carregarDados();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Alterar Vaga!: " + ex);
            }
            finally
            {
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                alterarvaga.StatusVaga1 = "Excluida";
                alterarvaga.ExcluirVaga();
                MessageBox.Show("Vaga Excluido!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                carregarDados();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Manobrista!: " + ex);
            }
            finally
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                novavaga = new Model.Vaga();
                novavaga.NumeroVaga1 = Convert.ToInt32(VagatextBox.Text);
                novavaga.StatusVaga1 = "Livre";
                novavaga.TipoVaga1 = TipocomboBox.SelectedItem.ToString();
                novavaga.InserirVaga();
                MessageBox.Show("Nova Vaga Inserida!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                carregarDados();
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar Vaga!: " + ex);
            }
            finally
            {
            }
        }
    }
}
