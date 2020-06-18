using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstacionamentoAtual.View
{
    public partial class Relatorios : Form
    {
        public View.PainelAdmin paineladmin;
        public Relatorios()
        {
            InitializeComponent();
        }
        public void Relatorio(DateTime Datainicial, DateTime DataFinal)
        {
            Document doc = new Document(PageSize.A4.Rotate());
            doc.SetMargins(0, 0, 40, 0);
            string caminho = @"C:\Users\Ale\Documents\PDFrelatorio.pdf";

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
            doc.Open();
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(@"C:\Users\Ale\Desktop\Projeto Programação\EstacionamentoAtual\EstacionamentoAtual\Img\sinalizacao.png");
            img.ScaleAbsolute(40f, 40f);
            img.Alignment = Element.ALIGN_CENTER;

            Paragraph titulo = new Paragraph();
            Paragraph pula1 = new Paragraph();
            Paragraph tiporelatorio = new Paragraph();
            Paragraph pula2 = new Paragraph();
            Paragraph dataemissao = new Paragraph();
            dataemissao.Alignment = Element.ALIGN_TOP;
            titulo.Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 40);
            titulo.Alignment = Element.ALIGN_CENTER;
            dataemissao.Add("                                                                                                                                                                                                                      "+DateTime.Now.ToString());
            titulo.Add("\n");
            titulo.Add("\n");
            titulo.Add("**BLOW PARK**");
            titulo.Add("\n");
            pula2.Alignment = Element.ALIGN_CENTER;
            pula2.Add("\n");
            doc.Add(dataemissao);
            doc.Add(img);
            doc.Add(titulo);
            doc.Add(pula2);
            tiporelatorio.Font = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 40);
            tiporelatorio.Alignment = Element.ALIGN_CENTER;
            tiporelatorio.Add("\n");
            tiporelatorio.Add("Relatório de Saídas");
            pula1.Alignment = Element.ALIGN_CENTER;
            pula1.Add("\n");
            pula1.Add("\n");
            doc.Add(tiporelatorio);
            doc.Add(pula1);

            Model.RelatorioPagamentos valores = new Model.RelatorioPagamentos();
            valores.Datainicial = Datainicial.Date;
            valores.Datafinal = DataFinal.Date;
            List<Model.RelatorioPagamentos> listavalores = new List<Model.RelatorioPagamentos>();
            listavalores = valores.RetornarValoresRelatorio();

            //PdfPTable table = new PdfPTable(8);
            PdfPTable table = new PdfPTable(new float[] {15f, 20f, 20f, 35f , 35f, 25f, 20f, 20f});
            table.DefaultCell.HorizontalAlignment = 1;
            table.DefaultCell.VerticalAlignment = 5;
            table.DefaultCell.Border = 2;
            BaseColor cor = new BaseColor(230, 232, 250);
            table.DefaultCell.BackgroundColor = cor;
            //BaseColor cor = new BaseColor(230, 232, 250);
            //table.DefaultCell.BackgroundColor = cor;
            //table.DefaultCell.
            
            table.AddCell("Vaga");
            table.AddCell("Cliente");
            table.AddCell("Placa");
            table.AddCell("Hora Entrada");
            table.AddCell("Hora Saida");
            table.AddCell("Tempo Permanecido");
            table.AddCell("Valor R$");
            table.AddCell("Pagamento");
            foreach (Model.RelatorioPagamentos rel in listavalores)
            {
                table.AddCell(rel.Numerovaga.ToString());
                table.AddCell(rel.Cliente.ToString());
                table.AddCell(rel.Placa.ToString());
                table.AddCell(rel.Data_Entrada1.ToString());
                table.AddCell(rel.Data_Saida1.ToString());
                table.AddCell(rel.Tempo_Permanecido1.ToString());
                table.AddCell(rel.Valor.ToString("n2"));
                table.AddCell(rel.Tipo_Pagamento1.ToString());
            }
            doc.Add(table);
            doc.Close();
            System.Diagnostics.Process.Start(caminho);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Relatorio(Convert.ToDateTime(DataInicialmaskedTextBox.Text), Convert.ToDateTime(DataFinalTextBox.Text));
        }
    }
}
