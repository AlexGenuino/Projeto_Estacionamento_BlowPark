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
    public partial class PainelAdmin : Form
    {
        public Menu menu;
        public PainelAdmin()
        {
            InitializeComponent();
        }

        private void PainelAdmin_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            menu.carregarDados();
            menu.Visible = true;
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            View.CadastrarManobrista cadastrarmanobrista = new View.CadastrarManobrista();
            cadastrarmanobrista.paineladmin = this;
            cadastrarmanobrista.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            View.CadastrarVaga cadastrovaga = new View.CadastrarVaga();
            cadastrovaga.paineladmin = this;
            cadastrovaga.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            View.CadastrarUsuario cadastrousuario = new View.CadastrarUsuario();
            cadastrousuario.paineladmin = this;
            cadastrousuario.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            View.Relatorios relatorio = new View.Relatorios();
            relatorio.paineladmin = this;
            relatorio.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btprecohora_Click(object sender, EventArgs e)
        {
            View.PrecoHora precohora = new View.PrecoHora();
            precohora.paineladmin = this;
            precohora.Visible = true;
        }
    }
}
