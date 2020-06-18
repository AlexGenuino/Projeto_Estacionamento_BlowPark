using EstacionamentoAtual.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoAtual.Model
{
    public class PrecoHora
    {

        private String DiaSemana;
        private double ValorPrimeiraHora;
        private double ValorDemaisHoras;
        private double ValorDiaria;
        private int Id_DiaSemana;

        public PrecoHora()
        {

        }

        DAO.PrecoHoraDAO pdao;

        public string DiaSemana1 { get => DiaSemana; set => DiaSemana = value; }
        public double ValorPrimeiraHora1 { get => ValorPrimeiraHora; set => ValorPrimeiraHora = value; }
        public double ValorDemaisHoras1 { get => ValorDemaisHoras; set => ValorDemaisHoras = value; }
        public int Id_DiaSemana1 { get => Id_DiaSemana; set => Id_DiaSemana = value; }
        public double ValorDiaria1 { get => ValorDiaria; set => ValorDiaria = value; }

        public List<PrecoHora> ListarPrecos()
        {

            pdao = new DAO.PrecoHoraDAO();
            return pdao.ListarPrecos(this);
        }

        public PrecoHora ProcurarDiaDaSemana()
        {
            pdao = new DAO.PrecoHoraDAO();
            return pdao.ProcurarDiaDaSemana(this);
        }

        public void AlterarDiaDaSemana()
        {
            pdao = new DAO.PrecoHoraDAO();
            pdao.AlterarDiaDaSemana(this);
        }

        public void InserirDiaDaSemana()
        {
            pdao = new DAO.PrecoHoraDAO();
            pdao.InserirDiaDaSemana(this);
        }
    }
}
