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
    public partial class CadastrarVagaLote : Form
    {
        public CadastrarVaga cadastrarvaga;
        Model.Vaga novavaga;
        public CadastrarVagaLote()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cadastrarvaga.Visible = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = Convert.ToInt32(txtnumeroinicial.Text); i < Convert.ToInt32(txtnumerofinal.Text)+1; i++)
                {
                    novavaga = new Model.Vaga();
                    novavaga.NumeroVaga1 = (i);
                    novavaga.StatusVaga1 = "Livre";
                    novavaga.TipoVaga1 = TipocomboBox.SelectedItem.ToString();
                    novavaga.InserirVaga();
                }
                MessageBox.Show("Vagas Cadastradas!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TipocomboBox.SelectedIndex = -1;
                txtnumeroinicial.Clear();
                txtnumerofinal.Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro ao Cadastrar Vagas!: " + ex);
            }
            finally
            {
            }
        }
    }
}
