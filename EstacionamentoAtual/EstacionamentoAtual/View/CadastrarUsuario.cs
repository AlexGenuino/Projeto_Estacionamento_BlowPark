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
    public partial class CadastrarUsuario : Form
    {
        public View.PainelAdmin paineladmin;
        private Conexao.Conexao conexao;
        private Int32 catchRowIndex;
        private Usuario alterarusuario;
        private Usuario novologin;

        public CadastrarUsuario()
        {
            InitializeComponent();
            carregarDados();
            alterarusuario = new Usuario();
        }

        public void carregarDados()
        {

            conexao = new Conexao.Conexao();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            string connectionString = conexao.getConnectionString();
            string query = "SELECT * FROM login where Status = 'Ativo' or Status = 'Inativo'";

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

                            dataGridView1.Rows.Add(dataTable.Rows[i][0], dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3], dataTable.Rows[i][4]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex);
                    }
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            paineladmin.Visible = true;
            this.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void preenchercombo()
        {
            txtlogin.Text = alterarusuario.Login1;
            txtsenha.Text = alterarusuario.Senha1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            catchRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                
                alterarusuario.Idlogin = Convert.ToInt32(row.Cells[0].Value.ToString());
                alterarusuario.Login1 = row.Cells[1].Value.ToString();
                alterarusuario.Senha1 = row.Cells[2].Value.ToString();
                alterarusuario.Permissao1 = row.Cells[3].Value.ToString();
                alterarusuario.Status = row.Cells[4].Value.ToString();
                preenchercombo();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                novologin = new Usuario();
                novologin.Login1 = txtlogin.Text;
                novologin.Senha1 = txtsenha.Text;
                novologin.Permissao1 = permissaocombobox.SelectedItem.ToString();
                novologin.Status = "Ativo";
                novologin.IserirLogin();
                MessageBox.Show("Novo Login Inserido!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                carregarDados();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar Manobrista!: " + ex);
            }
            finally
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                alterarusuario.Login1 = txtlogin.Text;
                alterarusuario.Senha1 = txtsenha.Text;
                alterarusuario.AlterarLogin();
                MessageBox.Show("Login Alterado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                carregarDados();
                statuscombobox.Text = "";
                permissaocombobox.Text = "";
                txtlogin.Text = "";
                txtsenha.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Alterar Manobrista!: " + ex);
            }
            finally
            {
            }
        }

        private void permissaocombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            alterarusuario.Permissao1 = permissaocombobox.SelectedItem.ToString();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            alterarusuario.Status = statuscombobox.SelectedItem.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                alterarusuario.Status = "Excluido";
                alterarusuario.ExcluirLogin();
                MessageBox.Show("Login Excluido!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                carregarDados();
                statuscombobox.Text = "";
                permissaocombobox.Text = "";
                txtlogin.Text = "";
                txtsenha.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Manobrista!: " + ex);
            }
            finally
            {
            }
        }
    }
}
