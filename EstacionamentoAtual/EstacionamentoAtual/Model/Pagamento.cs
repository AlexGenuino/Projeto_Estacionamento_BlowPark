using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    abstract class Pagamento
    {
        private String Cliente;
        private Double Valor;
        private int NumeroVaga;
        private int idsaida;
        private String Placa;

        public Pagamento()
        {

        }

        public string Cliente1 { get => Cliente; set => Cliente = value; }
        public double Valor1 { get => Valor; set => Valor = value; }
        public int NumeroVaga1 { get => NumeroVaga; set => NumeroVaga = value; }
        public int Idsaida { get => idsaida; set => idsaida = value; }
        public string Placa1 { get => Placa; set => Placa = value; }

        
    }
}
