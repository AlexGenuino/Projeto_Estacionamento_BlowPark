using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    public class TipoVeiculo : Veiculo
    {
        private String Tipo;
        private int idtipo;

        DAO.VeiculoDAO cdao = new DAO.VeiculoDAO();

        public string Tipo1 { get => Tipo; set => Tipo = value; }
        public int Idtipo { get => idtipo; set => idtipo = value; }

        public void InserirVeiculo()
        {
            cdao = new DAO.VeiculoDAO();
            cdao.InserirVeiculo(this);
        }

        public List<TipoVeiculo> ListarVeiculos()
        {

            cdao = new DAO.VeiculoDAO();
            return cdao.ListarVeiculos(this);
        }
        public List<TipoVeiculo> ListarVeiculosEstacionados()
        {

            cdao = new DAO.VeiculoDAO();
            return cdao.ListarVeiculosEstacionados();
        }

        public TipoVeiculo ProcurarVeiculo()
        {
            cdao = new DAO.VeiculoDAO();
            return cdao.ProcurarVeiculo(this);
        }
        public void AlterarVeiculo()
        {
            cdao = new DAO.VeiculoDAO();
            cdao.AlterarVeiculo(this);
        }
    }
}
