using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    class EntradaVaga : Vaga
    {
        private DateTime datahoraentrada;
        private int identrada;
        private int id_vaga;
        private int id_veiculo;
        private int id_manobrista;


        DAO.EntradaVagaDAO entradadao;
        public EntradaVaga()
        {

        }

        public DateTime Datahoraentrada { get => datahoraentrada; set => datahoraentrada = value; }
        public int Identrada { get => identrada; set => identrada = value; }
        public int Id_vaga { get => id_vaga; set => id_vaga = value; }
        public int Id_veiculo { get => id_veiculo; set => id_veiculo = value; }
        public int Id_manobrista { get => id_manobrista; set => id_manobrista = value; }


        public void InserirEntrada()
        {
            entradadao = new DAO.EntradaVagaDAO();
            entradadao.InserirEntrada(this);
        }

    }
}
