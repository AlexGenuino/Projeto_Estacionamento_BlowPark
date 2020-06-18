using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    public class Cliente : Pessoa
    {
        private String CPFcliente;
        private int idLogin;
        private String status;

        DAO.ClienteDAO cdao;

        public Cliente()
        {

        }

        public String CPFcliente1 { get => CPFcliente; set => CPFcliente = value; }
        public int IdLogin { get => idLogin; set => idLogin = value; }
        public string Status { get => status; set => status = value; }

        public void InserirCliente()
        {
            cdao = new DAO.ClienteDAO();
            cdao.InserirCliente(this);

        }

        public Cliente ProcurarCliente()
        {
            cdao = new DAO.ClienteDAO();
            return cdao.ProcurarCliente(this);
        }

        public void AtualizarCliente()
        {
            cdao = new DAO.ClienteDAO();
            cdao.AtualizarCliente(this);
        }

        public void DeletarCliente()
        {
            cdao = new DAO.ClienteDAO();
            //cdao.InserirCliente(this);
        }
    }
}
