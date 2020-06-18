using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    class RelatorioPagamentos
    {

        private int numerovaga;
        private String cliente;
        private String placa;
        private DateTime Data_Entrada;
        private DateTime Data_Saida;
        private String Tempo_Permanecido;
        private double valor;
        private String Tipo_Pagamento;
        private DateTime datainicial;
        private DateTime datafinal;

        DAO.RelatoriosDAO relatorios;

        public int Numerovaga { get => numerovaga; set => numerovaga = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public string Placa { get => placa; set => placa = value; }
        public DateTime Data_Entrada1 { get => Data_Entrada; set => Data_Entrada = value; }
        public DateTime Data_Saida1 { get => Data_Saida; set => Data_Saida = value; }
        public string Tempo_Permanecido1 { get => Tempo_Permanecido; set => Tempo_Permanecido = value; }
        public double Valor { get => valor; set => valor = value; }
        public string Tipo_Pagamento1 { get => Tipo_Pagamento; set => Tipo_Pagamento = value; }
        public DateTime Datainicial { get => datainicial; set => datainicial = value; }
        public DateTime Datafinal { get => datafinal; set => datafinal = value; }

        public List<RelatorioPagamentos> RetornarValoresRelatorio()
        {

            relatorios = new DAO.RelatoriosDAO();
            return relatorios.RetornarValoresRelatorio(this);
        }
    }
}
