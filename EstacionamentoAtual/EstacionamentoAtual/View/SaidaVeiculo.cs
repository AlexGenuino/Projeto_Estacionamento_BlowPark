using EstacionamentoAtual.DAO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstacionamentoAtual.View
{
    public partial class SaidaVeiculo : Form
    {
        public Menu menu;
        private Conexao.Conexao conexao;
        private Int32 catchRowIndex;
        private Model.SaidaVaga saidaveiculo;
        private Model.PrecoHora retornardiasemana;
        private Model.PrecoHora retornarvalordiaria;
        private List<Model.Manobrista> listmanobrista;

        public SaidaVeiculo()
        {
            InitializeComponent();
            carregarDados();
            saidaveiculo = new Model.SaidaVaga();
            PreencherManobrista();
        }
        private void PreencherManobrista()
        {
            listmanobrista = new List<Model.Manobrista>();
            Model.Manobrista manobrista = new Model.Manobrista();
            listmanobrista = manobrista.ListarManobrista();
            ManobristacomboBox.DataSource = listmanobrista;
            ManobristacomboBox.DisplayMember = "Nome1";
            ManobristacomboBox.ValueMember = "Idmanobrista";

        }

        private bool VerificarPreenchimento()
        {
            if(TipoPagamentocomboBox.SelectedItem != null && ManobristacomboBox.SelectedItem != null && saidaveiculo.Cliente1 != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void InserirSaida()
        {
            try
            {
                if (VerificarPreenchimento() == false)
                {
                    MessageBox.Show("Verificar Preenchimento");
                }
                else
                {
                    Model.Vaga vagalivre = new Model.Vaga();
                    saidaveiculo.Datahorasaida = DateTime.Now;
                    vagalivre.Idvaga = saidaveiculo.Idvaga;
                    saidaveiculo.Valor = CalcularHoraPreco();
                    saidaveiculo.TipoPagamento1 = TipoPagamentocomboBox.SelectedItem.ToString();
                    saidaveiculo.InserirSaida();
                    vagalivre.VagaLivre();
                    menu.carregarDados();
                    MessageBox.Show("Saida Realizada as " + DateTime.Now);
                    carregarDados();
                    TicketSaida(saidaveiculo.NumeroVaga1.ToString(), saidaveiculo.Datahoraentrada, saidaveiculo.Valor.ToString("n2"), saidaveiculo.Placa1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Procurar Cliente!: " + ex);
            }
        }

        public void TicketSaida(String vaga, DateTime DataEntrada, String valor, string placa)
        {
            Document doc = new Document(PageSize.A8);
            doc.SetMargins(0, 0, 0, 0);
            string caminho = @"C:\Users\Ale\Documents\PDF" + "relatorio.pdf";

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();
            DateTime DataSaida = DateTime.Now;
            System.TimeSpan tempopermanecido = DataSaida.Subtract(DataEntrada);
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
            paragrafo.Add("Tempo:" + (tempopermanecido.ToString(@"dd") + " Dia " + tempopermanecido.ToString(@"hh") + " Horas " + tempopermanecido.ToString(@"mm") + " Minutos " + tempopermanecido.ToString(@"ss") + " Segundos ") + "\n");
            paragrafo.Add("Data de Saida: " + DataSaida.Date.ToString(@"dd/MM/yyyy") + "\n" + " Hora: " + DataSaida.Hour + ":" + DataSaida.Minute + ":" + DataSaida.Second + "\n");
            paragrafo.Add(" |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            paragrafo.Add("           Vaga: " + vaga + "" + "\n");
            paragrafo.Add("           Placa: " + placa + "" + "\n");
            paragrafo.Add("           Valor: R$"+ valor + "" + "\n");
            paragrafo.Add("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
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
        public double CalcularHoraPreco()
        {
            DateTime DataHoraAgora = DateTime.Now;
            System.DateTime horaentrada = new System.DateTime();
            horaentrada = saidaveiculo.Datahoraentrada;
            System.DateTime horasaida = new System.DateTime();
            horasaida = DataHoraAgora;
            System.TimeSpan diferenca = horasaida.Subtract(horaentrada);
            //labelhoraspermanecidas.Text = diferenca.ToString(@"dd\:hh\:mm\:ss");
            if (diferenca.Days > 0)
            {
                if (diferenca.Days > 1)
                {
                    labelhoraspermanecidas.Text = (diferenca.ToString(@"dd") + " Dias " + diferenca.ToString(@"hh") + " Horas " + diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                    if (diferenca.Hours == 0)
                    {
                        labelhoraspermanecidas.Text = (diferenca.ToString(@"dd") + " Dias " + diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                    }
                    else
                    {
                        if (diferenca.Hours > 1)
                        {
                            labelhoraspermanecidas.Text = (diferenca.ToString(@"dd") + " Dias " + diferenca.ToString(@"hh") + " Horas " + diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                        }
                        else
                        {
                            if (diferenca.Hours == 1)
                            {
                                labelhoraspermanecidas.Text = (diferenca.ToString(@"dd") + " Dias " + diferenca.ToString(@"hh") + " Hora " + diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                            }
                        }
                    }
                }
                if (diferenca.Days == 1)
                {
                    labelhoraspermanecidas.Text = (diferenca.ToString(@"dd") + " Dia " + diferenca.ToString(@"hh") + " Horas " + diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                    if (diferenca.Hours == 0)
                    {
                        labelhoraspermanecidas.Text = (diferenca.ToString(@"dd") + " Dia " + diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                    }
                    else
                    {
                        if (diferenca.Hours > 1)
                        {
                            labelhoraspermanecidas.Text = (diferenca.ToString(@"dd") + " Dia " + diferenca.ToString(@"hh") + " Horas " + diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                        }
                        else
                        {
                            if (diferenca.Hours == 1)
                            {
                                labelhoraspermanecidas.Text = (diferenca.ToString(@"dd") + " Dia " + diferenca.ToString(@"hh") + " Hora " + diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                            }
                        }
                    }
                }
            }
            else
            {
                if (diferenca.Hours > 0)
                {
                    if (diferenca.Hours > 1)
                    {
                        labelhoraspermanecidas.Text = (diferenca.ToString(@"hh") + " Horas " + diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                    }
                    else
                    {
                        if (diferenca.Hours == 1)
                        {
                            labelhoraspermanecidas.Text = (diferenca.ToString(@"hh") + " Hora " + diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                        }
                    }
                }
                else
                {
                    labelhoraspermanecidas.Text = (diferenca.ToString(@"mm") + " Minutos " + diferenca.ToString(@"ss") + " Segundos ");
                }
            }
            double valorprimeirahora = RetornarValoresDiaDaSemana().ValorPrimeiraHora1;
            double valordemaishoras = RetornarValoresDiaDaSemana().ValorDemaisHoras1;
            double valorminuto = (valordemaishoras / 60);
            if (diferenca.TotalMinutes > 60 && diferenca.TotalMinutes < 1440)
            {
                double valorpago = (valorprimeirahora) + (diferenca.TotalMinutes - 60) * valorminuto;
                labelvalorpaga.Text = valorpago.ToString("n2");
                return valorpago;

            }
            else
            {
                if (diferenca.TotalMinutes > 1440)
                {
                    DateTime dataatual = horaentrada;
                    double valordiarias = 0;
                    for (int i = 1; i <= diferenca.Days; i++)
                    {
                        valordiarias += RetornarValoresDiarias(dataatual.AddDays(i).DayOfWeek.ToString()).ValorDiaria1;
                    }
                    int diaspassados = diferenca.Days;
                    double minutospassados = (diferenca.TotalMinutes - (diferenca.Days * 1440));
                    double valorpago2 = (valordiarias) + (minutospassados * valorminuto);
                    labelvalorpaga.Text = "R$ " + valorpago2.ToString("n2");
                    return valorpago2;
                }
                else
                {
                    double valorpago = valorprimeirahora;
                    labelvalorpaga.Text = "R$ " + valorpago.ToString("n2");
                    return valorpago;
                }
               
            }
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

        public Model.PrecoHora RetornarValoresDiarias(String DiaDaSemana)
        {
            retornarvalordiaria = new Model.PrecoHora();
            retornarvalordiaria.DiaSemana1 = DiaDaSemana;
            retornarvalordiaria = retornarvalordiaria.ProcurarDiaDaSemana();
            return retornarvalordiaria;
        }

        public void carregarDados()
        {
            conexao = new Conexao.Conexao();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            string connectionString = conexao.getConnectionString();
            string query = "SELECT a.Id_Entrada, a.Data_Hora, a.IdManobrista, a.Id_Veiculo, c.Placa, c.Modelo, c.Marca, c.CPF, d.Nome, a.Id_Vaga, b.NumeroVaga, b.Status, f.Data_Hora from entrada a INNER JOIN vaga b ON b.Id_Vaga = a.Id_Vaga INNER JOIN veiculo c ON c.Id_Veiculo = a.Id_Veiculo INNER JOIN cliente d on d.CPF = c.CPF LEFT JOIN saida f on f.Id_Entrada = a.Id_Entrada where b.Status = 'Ocupado' and f.Data_Hora is null";
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
                                dataGridView1.Rows.Add(dataTable.Rows[i][0], img, dataTable.Rows[i][1], dataTable.Rows[i][2], dataTable.Rows[i][3], dataTable.Rows[i][4], dataTable.Rows[i][5], dataTable.Rows[i][6], dataTable.Rows[i][7], dataTable.Rows[i][8], dataTable.Rows[i][9], dataTable.Rows[i][10], dataTable.Rows[i][11]);
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            menu.Visible = true;
            this.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            catchRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                labelvaga.Text = row.Cells[11].Value.ToString();
                labelveiculomodelo.Text = row.Cells[6].Value.ToString();
                labelmarca.Text = row.Cells[7].Value.ToString();
                labelplaca.Text = row.Cells[5].Value.ToString();
                labelcliente.Text = row.Cells[9].Value.ToString();
                saidaveiculo.Cliente1 = row.Cells[9].Value.ToString();
                saidaveiculo.Placa1 = row.Cells[5].Value.ToString();
                saidaveiculo.Identrada = Convert.ToInt32(row.Cells[0].Value.ToString());
                saidaveiculo.Idvaga = Convert.ToInt32(row.Cells[10].Value.ToString());
                saidaveiculo.NumeroVaga1 = Convert.ToInt32(row.Cells[11].Value.ToString());
                labeldatahoraentrada.Text = row.Cells[2].Value.ToString();
                saidaveiculo.Datahoraentrada = Convert.ToDateTime(row.Cells[2].Value.ToString());
                CalcularHoraPreco();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //String placa = "wjb7412";
            //String tipoveiculo = "Carro";
            //int vaga = 2;
            //TicketEntrada(placa, tipoveiculo, vaga);
        }

        private void ManobristacomboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Model.Manobrista mano1 = (Model.Manobrista)ManobristacomboBox.SelectedItem;
            saidaveiculo.Idmanobrista = mano1.Idmanobrista;
        }

        private void btnRegistrarSaida_Click(object sender, EventArgs e)
        {
            InserirSaida();
        }
    }
}
