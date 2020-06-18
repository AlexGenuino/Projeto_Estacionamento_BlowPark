using EstacionamentoAtual.DAO;
using EstacionamentoAtual.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstacionamentoAtual.View
{


    public partial class EntradaVeiculo : Form
    {
        public Menu menu;
        Cliente novocliente;
        Cliente encontrarcliente;
        TipoVeiculo novoveiculo;
        DAO.ClienteDAO Clientedao = new ClienteDAO();
        private Manobrista manobrista;
        private List<Manobrista> Manobrista;
        private Conexao.Conexao conexao;
        private List<TipoVeiculo> Veiculos;
        private TipoVeiculo veiculos;
        private Int32 catchRowIndex;
        private EntradaVaga novaentrada;
        private Vaga ocuparvaga;
        private Model.PrecoHora retornardiasemana;

        internal List<Manobrista> Manobrista1 { get => Manobrista; set => Manobrista = value; }
        internal List<TipoVeiculo> Veiculos1 { get => Veiculos; set => Veiculos = value; }

        public EntradaVeiculo()
        {

            InitializeComponent();
            novaentrada = new EntradaVaga();
            PreencherManobrista();
            carregarDados();
        }

        public Model.PrecoHora RetornarValoresDiaDaSemana()
        {
            DateTime retornardia = DateTime.Now;
            String diasemana = retornardia.DayOfWeek.ToString();
            retornardiasemana = new Model.PrecoHora();
            retornardiasemana.DiaSemana1 = diasemana;
            retornardiasemana = retornardiasemana.ProcurarDiaDaSemana();
            return retornardiasemana;
        }

        private bool VerificarPreenchimentoNulo()
        {
            if (txtcpfcliente.Text != "" && txtnomecliente.Text != "" && txtplaca.Text != "" && TipoVeiculoComboBox.SelectedItem != null && modelotxt.Text != "" && MarcaComboBox.SelectedItem != null && ManobristacomboBox.SelectedItem != null && numerovagalabel.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool VerificarVaga()
        {
            if(novaentrada.TipoVaga1 == TipoVeiculoComboBox.SelectedItem.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static System.Drawing.Image RedimensionarImagem(System.Drawing.Image srcImage, int newWidth, int newHeight, PixelFormat pf)
        {
            if (srcImage != null)
            {
                Bitmap newImage = new Bitmap(newWidth, newHeight, pf);
                using (Graphics gr = Graphics.FromImage((System.Drawing.Image)newImage))
                {
                    //gr.SmoothingMode = SmoothingMode.HighQuality;
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gr.DrawImage(srcImage, 0, 0, newWidth, newHeight);// new Rectangle(0, 0, newWidth, newHeight));
                    gr.Dispose();
                    return (System.Drawing.Image)newImage;
                }
            }
            else
                return null;

        }
        public void TicketEntrada(String Placa, String TipoVeiculo, int Vaga)
        {
            Document doc = new Document(PageSize.A8);
            doc.SetMargins(0, 0, 0, 0);
            string caminho = @"C:\Users\Ale\Documents\PDF" + Placa+"relatorio.pdf";

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();
            DateTime DataEntrada = DateTime.Now;
            //PdfPTable table = new PdfPTable(new float[] { 45f, 400f});
            PdfPTable table = new PdfPTable(1);
            //System.Drawing.Image img2 = System.Drawing.Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\iconeentrar - relatorio.png");
            //string simg = @"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\sinalizacao.png";
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\sinalizacao.png");
            img.ScaleAbsolute(40f, 40f);
            double primeira = RetornarValoresDiaDaSemana().ValorPrimeiraHora1;
            double demaishoras = RetornarValoresDiaDaSemana().ValorDemaisHoras1;
            Paragraph titulo = new Paragraph();
            img.Alignment = Element.ALIGN_CENTER;
            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.Add(img);
            doc.Add(titulo);
            Paragraph paragrafo = new Paragraph();
            paragrafo.Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12);
            Paragraph paragrafo2 = new Paragraph();
            paragrafo2.Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, (int)System.Drawing.FontStyle.Bold);
            paragrafo.Add(" Primeira Hora: R$ " + primeira + "\n");
            paragrafo.Add(" Demais Horas: R$ " + demaishoras + "\n");
            paragrafo.Add(" Data de Entrada: " + DataEntrada.Date.ToString(@"dd/MM/yyyy") + "\n" + " Hora: " + DataEntrada.Hour + ":" + DataEntrada.Minute + ":" + DataEntrada.Second + "\n");
            paragrafo.Add(" |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            paragrafo.Add("           Vaga: " + Vaga + "" + "\n");
            paragrafo.Add("           Placa: " + Placa + "" + "\n");
            paragrafo.Add("           Veiculo: " + TipoVeiculo + "" + "\n");
            paragrafo.Add(" |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            paragrafo.Alignment = Element.ALIGN_LEFT;
            paragrafo2.Add("**BLOW PARK**");
            paragrafo2.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragrafo2);
            doc.Add(paragrafo);


            //table.AddCell("******BLOW PARK******");

            //table.AddCell("\n" + "Primeira Hora: R$ " + primeira + "\n" + "Demais Horas: R$ " + demaishoras + "\n" + "\n" + "Data de Entrada: " + DataEntrada.Date.ToString(@"dd/MM/yyyy") + "\n" + "Hora: " + DataEntrada.Hour + ":" + DataEntrada.Minute + ":" + DataEntrada.Second + "\n" + "\n" + "Placa: " + Placa + "\n" + "Veiculo: " + TipoVeiculo);
            //table.AddCell("\n" + "Primeira Hora: R$ " + primeira + "\n" + "Demais Horas: R$ " + demaishoras + "\n" + "\n" + "Data de Entrada: " + DataEntrada.Date.ToString(@"dd/MM/yyyy") + "\n" + "Hora: " + DataEntrada.Hour + ":" + DataEntrada.Minute + ":" + DataEntrada.Second + "\n" + "\n" + "Placa: " + Placa + "\n" + "Veiculo: " + TipoVeiculo);

            //doc.Add(table);

            //string simg = "";
            //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(simg);
            //img.ScaleAbsolute(100, 130);
            //doc.Add(img);
            doc.Close();
            System.Diagnostics.Process.Start(caminho);

        }

        public void carregarDados()
        {
            conexao = new Conexao.Conexao();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            string connectionString = conexao.getConnectionString();
            string query = "SELECT * FROM vaga where Status = 'Livre'";
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
                                System.Drawing.Image img = System.Drawing.Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\trafficlight-green_40427.png");
                                dataGridView1.Rows.Add(dataTable.Rows[i][0], img, dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3]);
                            }
                            if (dataTable.Rows[i][2].Equals("Ocupado"))
                            {
                                System.Drawing.Image img = System.Drawing.Image.FromFile(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\trafficlight-red_40428.png");
                                dataGridView1.Rows.Add(dataTable.Rows[i][0], img, dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3]);
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

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            menu.Visible = true;
            this.Visible = false;
        }

        private void InserirNovoCliente()
        {
            try
            {
                novocliente = new Cliente();
                novocliente.Nome1 = txtnomecliente.Text;
                novocliente.CPFcliente1 = txtcpfcliente.Text;
                novocliente.IdLogin = Convert.ToInt32(menu.Loginv.Idlogin.ToString());
                novocliente.Status = "Ativo";
                novocliente.InserirCliente();
                MessageBox.Show("Cliente Cadastrado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar Cliente!: " + ex);
            }
            finally
            {
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void PreencherComboBox()
        {
            if (TipoVeiculoComboBox.SelectedItem.ToString().Equals("Carro"))
            {
                MarcaComboBox.Items.Clear();
                MarcaComboBox.Items.Add("HONDA");
                MarcaComboBox.Items.Add("FORD");
                MarcaComboBox.Items.Add("FIAT");
                MarcaComboBox.Items.Add("CITROEN");
                MarcaComboBox.Items.Add("HYUNDAI");
                MarcaComboBox.Items.Add("JEEP");
                MarcaComboBox.Items.Add("VOLKSWAGEN");
                MarcaComboBox.Items.Add("TOYOTA");
                MarcaComboBox.Items.Add("SUZUKI");
                MarcaComboBox.Items.Add("NISSAN");
                MarcaComboBox.Items.Add("CHEVROLET");
                MarcaComboBox.Items.Add("AUDI");
                MarcaComboBox.Items.Add("BMW");
                MarcaComboBox.Items.Add("PEUGEOT");
                MarcaComboBox.Items.Add("LAND ROVER");
                MarcaComboBox.Items.Add("CAOA CHERY");
            }
            else
            {
                MarcaComboBox.Items.Clear();
                MarcaComboBox.Items.Add("YAMAHA");
                MarcaComboBox.Items.Add("SUZUKI");
                MarcaComboBox.Items.Add("HONDA");
                MarcaComboBox.Items.Add("KAWASAKI");
                MarcaComboBox.Items.Add("DAFRA");
                MarcaComboBox.Items.Add("KASINSKI");
            }
        }
        private void InserirNovoVeiculo()
        {
            try
            {
                novoveiculo = new TipoVeiculo();
                novoveiculo.Cpfveiculo = txtcpfcliente.Text;
                novoveiculo.Placa1 = txtplaca.Text;
                novoveiculo.Marca1 = MarcaComboBox.SelectedItem.ToString();
                novoveiculo.Modelo1 = modelotxt.Text;
                novoveiculo.Tipo1 = TipoVeiculoComboBox.Text;
                novoveiculo.InserirVeiculo();
                MessageBox.Show("Veiculo Cadastrado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar Veiculo!: " + ex);
            }
            finally
            {

            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void MarcaComboBox_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void TipoVeiculoComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            PreencherComboBox();
        }
        int i = 0;
        private void button7_Click(object sender, EventArgs e)
        {
            Veiculos = new List<TipoVeiculo>();
            veiculos = new TipoVeiculo();
            veiculos.Cpfveiculo = txtcpfcliente.Text;
            Veiculos = veiculos.ListarVeiculos();
            if (i < Veiculos.Count)
            {
                txtplaca.Text = Veiculos[i].Placa1;
                veiculos.Placa1 = Veiculos[i].Placa1;
                TipoVeiculoComboBox.Text = Veiculos[i].Tipo1;
                veiculos.Tipo1 = Veiculos[i].Tipo1;
                modelotxt.Text = Veiculos[i].Modelo1;
                veiculos.Modelo1 = Veiculos[i].Modelo1;
                MarcaComboBox.Text = Veiculos[i].Marca1;
                veiculos.Marca1 = Veiculos[i].Marca1;
                veiculos.Idveiculo = Veiculos[i].Idveiculo;
                novaentrada.Id_veiculo = Veiculos[i].Idveiculo;
                i++;
            }
            else
            {
                i = 0;
                MessageBox.Show("Não há mais veiculos cadastrados!", "", MessageBoxButtons.OK);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                encontrarcliente = new Cliente();
                encontrarcliente.CPFcliente1 = txtcpfcliente.Text;
                encontrarcliente = encontrarcliente.ProcurarCliente();
                if (encontrarcliente != null)
                {
                    txtnomecliente.Text = encontrarcliente.Nome1;
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Procurar Cliente!: " + ex);
            }
            finally
            {

            }
        }

        private void PreencherManobrista()
        {
            Manobrista = new List<Manobrista>();
            manobrista = new Manobrista();
            Manobrista = manobrista.ListarManobrista();
            ManobristacomboBox.DataSource = Manobrista;
            ManobristacomboBox.DisplayMember = "Nome1";
            ManobristacomboBox.ValueMember = "Idmanobrista";

        }
        private void ManobristacomboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(Manobrista.Where(mano => mano.Idmanobrista == Convert.ToInt32(ManobristacomboBox.SelectedValue.ToString())).FirstOrDefault().Cpf);
            Manobrista mano1 = (Manobrista)ManobristacomboBox.SelectedItem;
            novaentrada.Id_manobrista = mano1.Idmanobrista;

        }
        private void ManobristacomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            catchRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                novaentrada.Id_vaga = Convert.ToInt32(row.Cells[0].Value.ToString());
                numerovagalabel.Text = row.Cells[2].Value.ToString();
                novaentrada.TipoVaga1 = row.Cells[4].Value.ToString();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (VerificarPreenchimentoNulo() == true && VerificarVaga() == true)
            {

                try
                {
                    encontrarcliente = new Cliente();
                    encontrarcliente.CPFcliente1 = txtcpfcliente.Text;
                    Model.TipoVeiculo procurarveiculo = new Model.TipoVeiculo();
                    procurarveiculo.Placa1 = txtplaca.Text;
                    procurarveiculo.Cpfveiculo = txtcpfcliente.Text;

                    if (encontrarcliente.ProcurarCliente() == null)
                    {
                        InserirNovoCliente();
                        InserirNovoVeiculo();
                        ocuparvaga = new Model.Vaga();
                        DateTime DataEntrada = DateTime.Now;
                        novaentrada.Datahoraentrada = DataEntrada;
                        ocuparvaga.StatusVaga1 = "Ocupado";
                        ocuparvaga.Idvaga = novaentrada.Id_vaga;
                        novaentrada.Id_veiculo = procurarveiculo.ProcurarVeiculo().Idveiculo;
                        ocuparvaga.VagaOcupada();
                        novaentrada.InserirEntrada();
                        carregarDados();
                        menu.carregarDados();
                        MessageBox.Show("Entrada Cadastrada as: " + DataEntrada, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TicketEntrada(txtplaca.Text, TipoVeiculoComboBox.SelectedItem.ToString(), Convert.ToInt32(numerovagalabel.Text));
                    }
                    else
                    {
                        if (encontrarcliente.ProcurarCliente().CPFcliente1 == txtcpfcliente.Text && procurarveiculo.ProcurarVeiculo() == null)
                        {
                            InserirNovoVeiculo();
                            ocuparvaga = new Model.Vaga();
                            DateTime DataEntrada = DateTime.Now;
                            novaentrada.Datahoraentrada = DataEntrada;
                            ocuparvaga.StatusVaga1 = "Ocupado";
                            ocuparvaga.Idvaga = novaentrada.Id_vaga;
                            novaentrada.Id_veiculo = procurarveiculo.ProcurarVeiculo().Idveiculo;
                            ocuparvaga.VagaOcupada();
                            novaentrada.InserirEntrada();
                            carregarDados();
                            menu.carregarDados();
                            MessageBox.Show("Entrada Cadastrada as: " + DataEntrada, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TicketEntrada(txtplaca.Text, TipoVeiculoComboBox.SelectedItem.ToString(), Convert.ToInt32(numerovagalabel.Text));
                        }
                        else
                        {
                            ocuparvaga = new Model.Vaga();
                            DateTime DataEntrada = DateTime.Now;
                            novaentrada.Datahoraentrada = DataEntrada;
                            ocuparvaga.StatusVaga1 = "Ocupado";
                            ocuparvaga.Idvaga = novaentrada.Id_vaga;
                            ocuparvaga.VagaOcupada();
                            novaentrada.InserirEntrada();
                            carregarDados();
                            menu.carregarDados();
                            MessageBox.Show("Entrada Cadastrada as: " + DataEntrada, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TicketEntrada(txtplaca.Text, TipoVeiculoComboBox.SelectedItem.ToString(), Convert.ToInt32(numerovagalabel.Text));
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar Veiculo!: " + ex);
                }
                finally
                {

                }
            }
            else
            {
                MessageBox.Show("Verificar Preenchimento");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtcpfcliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != 8))
            {

                e.Handled = true;
            }
        }
    }
}
