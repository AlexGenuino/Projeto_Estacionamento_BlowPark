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
using Ubiety.Dns.Core.Records;

namespace EstacionamentoAtual
{
    public partial class Menu : Form
    {

        private Conexao.Conexao conexao;
        private Model.Usuario loginv;
        private DAO.LoginDAO logindao;

        public Usuario Loginv { get => loginv; set => loginv = value; }

        public Menu(DAO.LoginDAO logindao)
        {
            InitializeComponent();
            this.logindao = logindao;
            carregarDados();
            preenchertxt();
            
        }

        public void preenchertxt()
        {
            loginv = new Model.Usuario();
            loginv = logindao.Novologin;
            labelmenu.Text = loginv.Permissao1.ToString();
            if(labelmenu.Text == "Funcionario")
            {
                button2.Enabled = false;
            }

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
                        DataGridViewImageColumn dimg = new DataGridViewImageColumn();
                        adapter.Fill(dataTable);
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            if (dataTable.Rows[i][2].Equals("Livre"))
                            {
                                Image img = Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\trafficlight-green_40427.png");
                                dataGridView1.Rows.Add(img, dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3]);
                            }
                            if (dataTable.Rows[i][2].Equals("Ocupado"))
                            {
                                Image img = Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\trafficlight-red_40428.png");
                                dataGridView1.Rows.Add(img, dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3]);
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
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           

        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            View.CadastrarVaga cadastrovaga = new View.CadastrarVaga();
            
            cadastrovaga.Visible = true;
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            View.EntradaVeiculo entrada = new View.EntradaVeiculo();
            entrada.menu = this;
            entrada.Visible = true;
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            View.SaidaVeiculo saida = new View.SaidaVeiculo();
            saida.menu = this;
            saida.Visible = true;
            this.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.labelhora.Text = DateTime.Now.ToString("HH:mm:ss");
            this.labeldata.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            View.PainelAdmin paineladmin = new View.PainelAdmin();
            paineladmin.menu = this;
            paineladmin.Visible = true;
            this.Visible = false;
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            View.ConsultarCliente consultacliente = new View.ConsultarCliente();
            consultacliente.menu = this;
            consultacliente.Visible = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            View.ConsultarVagas consultarvagas = new View.ConsultarVagas();
            consultarvagas.menu = this;
            consultarvagas.Visible = true;
            this.Visible = false;
        }
    }
}
