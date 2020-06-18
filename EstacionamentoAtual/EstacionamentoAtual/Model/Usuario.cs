using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
        public class Usuario
        {
            private String Login;
            private String Senha;
            private int idlogin;
            private String status;
            private String Permissao;
            public DAO.LoginDAO ldao;

            public Usuario()
            {

            }

            public string Login1 { get => Login; set => Login = value; }
            public string Senha1 { get => Senha; set => Senha = value; }
            public int Idlogin { get => idlogin; set => idlogin = value; }
            public string Permissao1 { get => Permissao; set => Permissao = value; }
             public string Status { get => status; set => status = value; }

        public void VerificarLogin()
       {
                ldao = new DAO.LoginDAO();
                ldao.VerificaLogin(this);
        }
        public void IserirLogin()
        {
            ldao = new DAO.LoginDAO();
            ldao.InserirLogin(this);
        }

        public void AlterarLogin()
        {
            ldao = new DAO.LoginDAO();
            ldao.AlterarLogin(this);
        }

        public void ExcluirLogin()
        {
            ldao = new DAO.LoginDAO();
            ldao.ExcluirLogin(this);
        }

    }

}

