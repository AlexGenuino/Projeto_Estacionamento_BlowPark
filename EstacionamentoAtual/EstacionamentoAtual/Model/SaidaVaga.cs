using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    class SaidaVaga : Vaga
    {
        private DateTime datahorasaida;
        private int idsaida;
        private String Placa;
        private int identrada;
        private String Cliente;
        private Double valor;
        private DateTime datahoraentrada;
        private int idmanobrista;
        private String TipoPagamento;

        DAO.SaidaVagaDAO sdao;

        public SaidaVaga()
        {

        }

        public DateTime Datahorasaida { get => datahorasaida; set => datahorasaida = value; }
        public int Idsaida { get => idsaida; set => idsaida = value; }
        public string Placa1 { get => Placa; set => Placa = value; }
        public string Cliente1 { get => Cliente; set => Cliente = value; }
        public double Valor { get => valor; set => valor = value; }
        public DateTime Datahoraentrada { get => datahoraentrada; set => datahoraentrada = value; }
        public int Idmanobrista { get => idmanobrista; set => idmanobrista = value; }
        public int Identrada { get => identrada; set => identrada = value; }
        public string TipoPagamento1 { get => TipoPagamento; set => TipoPagamento = value; }

        public void InserirSaida()
        {
            sdao = new DAO.SaidaVagaDAO();
            sdao.InserirSaida(this);
        }
    }
}
