using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    public abstract class Pessoa
    {
        private String Nome;
        private DateTime DataNasicmento;
       
        public string Nome1 { get => Nome; set => Nome = value; }
        public DateTime DataNasicmento1 { get => DataNasicmento; set => DataNasicmento = value; }
        
    }
}
