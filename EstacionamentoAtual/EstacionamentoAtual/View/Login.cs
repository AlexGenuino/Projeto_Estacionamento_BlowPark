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
    public partial class Login : Form
    {
        Model.Usuario login;
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnAcessar_Click(object sender, EventArgs e)
        {
            {
                login = new Model.Usuario();//Cria um Objeto para receber as entradas do usuario
                login.Login1 = txtusuario.Text;
                login.Senha1 = txtsenha.Text;
                try
                {
                    login.VerificarLogin();//chama o metodo login do objeto login que faz a verificação na Classe LoginDAO
                    this.Hide();
                    
                }
                catch
                {
                    MessageBox.Show("Usuario ou senha incorreto", "Erro ao autenticar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //Picture picture = new Picture();
            //picture.ShowDialog();
        }
    }
}
