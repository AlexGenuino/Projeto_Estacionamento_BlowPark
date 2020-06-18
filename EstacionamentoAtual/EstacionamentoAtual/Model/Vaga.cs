using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    class Vaga 
    {
        private int NumeroVaga;
        private String StatusVaga;
        private int idvaga;
        private String TipoVaga;
        DAO.VagaDAO vdao;

        public int NumeroVaga1 { get => NumeroVaga; set => NumeroVaga = value; }
        public String StatusVaga1 { get => StatusVaga; set => StatusVaga = value; }
        public int Idvaga { get => idvaga; set => idvaga = value; }
        public string TipoVaga1 { get => TipoVaga; set => TipoVaga = value; }

        public void InserirVaga()
        {
            vdao = new DAO.VagaDAO();
            vdao.InserirVaga(this);
        }
        public void AlterarVaga()
        {
            vdao = new DAO.VagaDAO();
            vdao.AlterarVaga(this);
        }

        public void ExcluirVaga()
        {
            vdao = new DAO.VagaDAO();
            vdao.ExcluirVaga(this);
        }

        public void VagaOcupada()
        {
            vdao = new DAO.VagaDAO();
            vdao.VagaOcupada(this);
        }

        public void VagaLivre()
        {
            vdao = new DAO.VagaDAO();
            vdao.VagaLivre(this);
        }
    }
}
