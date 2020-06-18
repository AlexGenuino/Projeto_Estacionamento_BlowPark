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
    public partial class PrecoHora : Form
    {
        public View.PainelAdmin paineladmin;
        List<Model.PrecoHora> listprecohora;
        Model.PrecoHora precohora;
        internal List<Model.PrecoHora> Listprecohora { get => listprecohora; set => listprecohora = value; }
        public PrecoHora()
        {
            InitializeComponent();
            precohora = new Model.PrecoHora();
            
            if(verificar() == true)
            {
                PreencherPrecos();
            }
            
        }

        private bool verificar()
        {
            listprecohora = new List<Model.PrecoHora>();
            listprecohora = precohora.ListarPrecos();

            if (listprecohora.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void PreencherPrecos()
        {
            listprecohora = precohora.ListarPrecos();
            segundaboxprimeira.Text = listprecohora[0].ValorPrimeiraHora1.ToString();
            segundaboxdemais.Text = listprecohora[0].ValorDemaisHoras1.ToString();
            segundaboxdiaria.Text = listprecohora[0].ValorDiaria1.ToString();
            tercaboxprimeira.Text = listprecohora[1].ValorPrimeiraHora1.ToString();
            tercaboxdemais.Text = listprecohora[1].ValorDemaisHoras1.ToString();
            tercaboxdiaria.Text = listprecohora[1].ValorDiaria1.ToString();
            quartaboxprimeira.Text = listprecohora[2].ValorPrimeiraHora1.ToString();
            quartaboxdemais.Text = listprecohora[2].ValorDemaisHoras1.ToString();
            quartaboxdiaria.Text = listprecohora[2].ValorDiaria1.ToString();
            quintaboxprimeira.Text = listprecohora[3].ValorPrimeiraHora1.ToString();
            quintaboxdemais.Text = listprecohora[3].ValorDemaisHoras1.ToString();
            quintaboxdiaria.Text = listprecohora[3].ValorDiaria1.ToString();
            sextaboxprimeira.Text = listprecohora[4].ValorPrimeiraHora1.ToString();
            sextaboxdemais.Text = listprecohora[4].ValorDemaisHoras1.ToString();
            sextaboxdiaria.Text = listprecohora[4].ValorDiaria1.ToString();
            sabadoboxprimeira.Text = listprecohora[5].ValorPrimeiraHora1.ToString();
            sabadoboxdemais.Text = listprecohora[5].ValorDemaisHoras1.ToString();
            sabadoboxdiaria.Text = listprecohora[5].ValorDiaria1.ToString();
            domingoboxprimeira.Text = listprecohora[6].ValorPrimeiraHora1.ToString();
            domingoboxdemais.Text = listprecohora[6].ValorDemaisHoras1.ToString();
            domingoboxdiaria.Text = listprecohora[6].ValorDiaria1.ToString();
        }

        private void Inserir()
        {
            try
            {
                Model.PrecoHora Segunda = new Model.PrecoHora();
                Segunda.DiaSemana1 = "Monday";
                Segunda.ValorPrimeiraHora1 = Convert.ToDouble(segundaboxprimeira.Text);
                Segunda.ValorDemaisHoras1 = Convert.ToDouble(segundaboxdemais.Text);
                Segunda.ValorDiaria1 = Convert.ToDouble(segundaboxdiaria.Text);
                Segunda.InserirDiaDaSemana();
                Model.PrecoHora Terca = new Model.PrecoHora();
                Terca.DiaSemana1 = "Tuesday";
                Terca.ValorPrimeiraHora1 = Convert.ToDouble(tercaboxprimeira.Text);
                Terca.ValorDemaisHoras1 = Convert.ToDouble(tercaboxdemais.Text);
                Terca.ValorDiaria1 = Convert.ToDouble(tercaboxdiaria.Text);
                Terca.InserirDiaDaSemana();
                Model.PrecoHora Quarta = new Model.PrecoHora();
                Quarta.DiaSemana1 = "Wednesday";
                Quarta.ValorPrimeiraHora1 = Convert.ToDouble(quartaboxprimeira.Text);
                Quarta.ValorDemaisHoras1 = Convert.ToDouble(quartaboxdemais.Text);
                Quarta.ValorDiaria1 = Convert.ToDouble(quartaboxdiaria.Text);
                Quarta.InserirDiaDaSemana();
                Model.PrecoHora Quinta = new Model.PrecoHora();
                Quinta.DiaSemana1 = "Thursday";
                Quinta.ValorPrimeiraHora1 = Convert.ToDouble(quintaboxprimeira.Text);
                Quinta.ValorDemaisHoras1 = Convert.ToDouble(quintaboxdemais.Text);
                Quinta.ValorDiaria1 = Convert.ToDouble(quintaboxdiaria.Text);
                Quinta.InserirDiaDaSemana();
                Model.PrecoHora Sexta = new Model.PrecoHora();
                Sexta.DiaSemana1 = "Friday";
                Sexta.ValorPrimeiraHora1 = Convert.ToDouble(sextaboxprimeira.Text);
                Sexta.ValorDemaisHoras1 = Convert.ToDouble(sextaboxdemais.Text);
                Sexta.ValorDiaria1 = Convert.ToDouble(sextaboxdiaria.Text);
                Sexta.InserirDiaDaSemana();
                Model.PrecoHora Sabado = new Model.PrecoHora();
                Sabado.DiaSemana1 = "Saturday";
                Sabado.ValorPrimeiraHora1 = Convert.ToDouble(sabadoboxprimeira.Text);
                Sabado.ValorDemaisHoras1 = Convert.ToDouble(sabadoboxdemais.Text);
                Sabado.ValorDiaria1 = Convert.ToDouble(sabadoboxdiaria.Text);
                Sabado.InserirDiaDaSemana();
                Model.PrecoHora Domingo = new Model.PrecoHora();
                Domingo.DiaSemana1 = "Sunday";
                Domingo.ValorPrimeiraHora1 = Convert.ToDouble(domingoboxprimeira.Text);
                Domingo.ValorDemaisHoras1 = Convert.ToDouble(domingoboxdemais.Text);
                Domingo.ValorDiaria1 = Convert.ToDouble(domingoboxdiaria.Text);
                Domingo.InserirDiaDaSemana();
                MessageBox.Show("Inserido com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Alterar!: " + ex);
            }
        }

        private void Alterar()
        {
            try
            {
                Model.PrecoHora Segunda = new Model.PrecoHora();
                Segunda.Id_DiaSemana1 = listprecohora[0].Id_DiaSemana1;
                Segunda.ValorPrimeiraHora1 = Convert.ToDouble(segundaboxprimeira.Text);
                Segunda.ValorDemaisHoras1 = Convert.ToDouble(segundaboxdemais.Text);
                Segunda.ValorDiaria1 = Convert.ToDouble(segundaboxdiaria.Text);
                Segunda.AlterarDiaDaSemana();
                Model.PrecoHora Terca = new Model.PrecoHora();
                Terca.Id_DiaSemana1 = listprecohora[1].Id_DiaSemana1;
                Terca.ValorPrimeiraHora1 = Convert.ToDouble(tercaboxprimeira.Text);
                Terca.ValorDemaisHoras1 = Convert.ToDouble(tercaboxdemais.Text);
                Terca.ValorDiaria1 = Convert.ToDouble(tercaboxdiaria.Text);
                Terca.AlterarDiaDaSemana();
                Model.PrecoHora Quarta = new Model.PrecoHora();
                Quarta.Id_DiaSemana1 = listprecohora[2].Id_DiaSemana1;
                Quarta.ValorPrimeiraHora1 = Convert.ToDouble(quartaboxprimeira.Text);
                Quarta.ValorDemaisHoras1 = Convert.ToDouble(quartaboxdemais.Text);
                Quarta.ValorDiaria1 = Convert.ToDouble(quartaboxdiaria.Text);
                Quarta.AlterarDiaDaSemana();
                Model.PrecoHora Quinta = new Model.PrecoHora();
                Quinta.Id_DiaSemana1 = listprecohora[3].Id_DiaSemana1;
                Quinta.ValorPrimeiraHora1 = Convert.ToDouble(quintaboxprimeira.Text);
                Quinta.ValorDemaisHoras1 = Convert.ToDouble(quintaboxdemais.Text);
                Quinta.ValorDiaria1 = Convert.ToDouble(quintaboxdiaria.Text);
                Quinta.AlterarDiaDaSemana();
                Model.PrecoHora Sexta = new Model.PrecoHora();
                Sexta.Id_DiaSemana1 = listprecohora[4].Id_DiaSemana1;
                Sexta.ValorPrimeiraHora1 = Convert.ToDouble(sextaboxprimeira.Text);
                Sexta.ValorDemaisHoras1 = Convert.ToDouble(sextaboxdemais.Text);
                Sexta.ValorDiaria1 = Convert.ToDouble(sextaboxdiaria.Text);
                Sexta.AlterarDiaDaSemana();
                Model.PrecoHora Sabado = new Model.PrecoHora();
                Sabado.Id_DiaSemana1 = listprecohora[5].Id_DiaSemana1;
                Sabado.ValorPrimeiraHora1 = Convert.ToDouble(sabadoboxprimeira.Text);
                Sabado.ValorDemaisHoras1 = Convert.ToDouble(sabadoboxdemais.Text);
                Sabado.ValorDiaria1 = Convert.ToDouble(sabadoboxdiaria.Text);
                Sabado.AlterarDiaDaSemana();
                Model.PrecoHora Domingo = new Model.PrecoHora();
                Domingo.Id_DiaSemana1 = listprecohora[6].Id_DiaSemana1;
                Domingo.ValorPrimeiraHora1 = Convert.ToDouble(domingoboxprimeira.Text);
                Domingo.ValorDemaisHoras1 = Convert.ToDouble(domingoboxdemais.Text);
                Domingo.ValorDiaria1 = Convert.ToDouble(domingoboxdiaria.Text);
                Domingo.AlterarDiaDaSemana();
                MessageBox.Show("Alteração Concluida!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Alterar!: " + ex);
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            paineladmin.Visible = true;
            this.Visible = false;
        }

        private void PrecoHora_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Alterar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inserir();
        }
    }
}
