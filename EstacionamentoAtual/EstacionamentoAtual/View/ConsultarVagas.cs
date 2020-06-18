using iTextSharp.text;
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
    public partial class ConsultarVagas : Form
    {
        private Conexao.Conexao conexao;
        public Menu menu;
        private Int32 catchRowIndex;
        private List<Model.TipoVeiculo> veiculosestacionados;
        private Model.TipoVeiculo veiculos;
        public ConsultarVagas()
        {
            InitializeComponent();
            carregarDados();
            veiculosestacionados = new List<Model.TipoVeiculo>();
            veiculos = new Model.TipoVeiculo();
            veiculosestacionados = veiculos.ListarVeiculosEstacionados();
        }

        public void carregarDados()
        {
            conexao = new Conexao.Conexao();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            string connectionString = conexao.getConnectionString();
            string query = "SELECT a.Id_Entrada, a.Data_Hora, a.IdManobrista, a.Id_Veiculo, c.Placa, c.Modelo, c.Marca, c.CPF, d.Nome, a.Id_Vaga, b.NumeroVaga, b.Status, f.Data_Hora, c.Tipo from entrada a INNER JOIN vaga b ON b.Id_Vaga = a.Id_Vaga INNER JOIN veiculo c ON c.Id_Veiculo = a.Id_Veiculo INNER JOIN cliente d on d.CPF = c.CPF LEFT JOIN saida f on f.Id_Entrada = a.Id_Entrada where b.Status = 'Ocupado' and f.Data_Hora is null";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                {
                    try
                    {
                        DataTable dataTable = new DataTable();
                        DataGridViewImageColumn dimg = new DataGridViewImageColumn();
                        adapter.Fill(dataTable);
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i][11].Equals("Ocupado"))
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\trafficlight-red_40428.png");
                                dataGridView1.Rows.Add(dataTable.Rows[i][0], img, dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3], dataTable.Rows[i][4], dataTable.Rows[i][5], dataTable.Rows[i][6], dataTable.Rows[i][7], dataTable.Rows[i][8], dataTable.Rows[i][9], dataTable.Rows[i][10], dataTable.Rows[i][11], dataTable.Rows[i][13]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex);
                    }
                }
            }
        }

        private System.Drawing.Image RetornarImagemVeiculo(String Modelo)
        {

            this.pictureBoxVeiculo.Location = new System.Drawing.Point(412, 110);
            this.pictureBoxVeiculo.Size = new System.Drawing.Size(250, 143);
            System.Drawing.Image camera1 = System.Drawing.Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\camera1.jpg");
            System.Drawing.Image camera2 = System.Drawing.Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\camera2.jpg");
            System.Drawing.Image camera3 = System.Drawing.Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\camera3.jpg");
            System.Drawing.Image camera4 = System.Drawing.Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\camera4.jpg");
            System.Drawing.Image camera5 = System.Drawing.Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\nenhum-video.png");
            Random numAleatorio = new Random();
            int valorInteiro = numAleatorio.Next(1, 6);
            if (valorInteiro == 1)
            {
                return camera1;
            }
            if (valorInteiro == 2)
            {
                return camera3;
            }
            if (valorInteiro == 3)
            {
                return camera3;
            }
            if (valorInteiro == 4)
            {
                return camera4;
            }
            else
            {
                return camera5;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            menu.Visible = true;
            this.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            menu.Visible = true;
            this.Visible = false;
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            catchRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                labelvaga.Text = row.Cells[11].Value.ToString();
                labelmodelo.Text = row.Cells[6].Value.ToString();
                labelmarca.Text = row.Cells[7].Value.ToString();
                labelplaca.Text = row.Cells[5].Value.ToString();
                labelcliente.Text = row.Cells[9].Value.ToString();
                labelcpfcliente.Text = row.Cells[8].Value.ToString();
                labelveiculo.Text = row.Cells[13].Value.ToString();
                pictureBoxVeiculo.Image = RetornarImagemVeiculo(row.Cells[6].Value.ToString());
            }
        }
    }
}

