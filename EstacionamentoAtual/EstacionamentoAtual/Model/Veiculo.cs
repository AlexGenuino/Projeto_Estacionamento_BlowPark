using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    public abstract class Veiculo
    {

        private String Marca;
        private String Modelo;
        private String Placa;
        private String cpfveiculo;
        private int idveiculo;
        

        public string Marca1 { get => Marca; set => Marca = value; }
        public string Modelo1 { get => Modelo; set => Modelo = value; }
        public string Placa1 { get => Placa; set => Placa = value; }
        public int Idveiculo { get => idveiculo; set => idveiculo = value; }
        public string Cpfveiculo { get => cpfveiculo; set => cpfveiculo = value; }

       
    }
}
