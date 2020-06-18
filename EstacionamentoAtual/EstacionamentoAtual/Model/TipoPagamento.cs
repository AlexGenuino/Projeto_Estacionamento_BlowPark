using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    class TipoPagamento : Pagamento
    {
        private String Tipo;
        private int idTipo;

        public TipoPagamento()
        {

        }

        public string Tipo1 { get => Tipo; set => Tipo = value; }
        public int IdTipo { get => idTipo; set => idTipo = value; }

        public void InserirPagamento()
        {

        }

        public void ExcluirPagamento()
        {

        }


    }
}
