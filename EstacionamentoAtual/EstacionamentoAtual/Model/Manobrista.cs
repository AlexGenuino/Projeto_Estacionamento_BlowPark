using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    class Manobrista : Pessoa
    {
        private string status;
        private int idmanobrista;
        private int idlogin;
        private string cpf;

        public Manobrista() 
        {

        }

        DAO.ManobristaDAO mdao;

        public string Status { get => status; set => status = value; }
        public int Idmanobrista { get => idmanobrista; set => idmanobrista = value; }
        public int Idlogin { get => idlogin; set => idlogin = value; }
        public string Cpf { get => cpf; set => cpf = value; }

        public void InserirManobrista()
        {
            mdao = new DAO.ManobristaDAO();
            mdao.InserirManobrista(this);

        }

        public void AlterarManobrista()
        {
            mdao = new DAO.ManobristaDAO();
            mdao.AlterarManobrista(this);
        }

        public void ExcluirManobrista()
        {
            mdao = new DAO.ManobristaDAO();
            mdao.ExcluirManobrista(this);
        }

        public List<Manobrista>ListarManobrista()
        {

            mdao = new DAO.ManobristaDAO();
            return mdao.ListarManobrista(this);
        }
    }
}
