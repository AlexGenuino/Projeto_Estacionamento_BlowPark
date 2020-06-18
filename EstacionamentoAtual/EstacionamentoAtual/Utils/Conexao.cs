using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EstacionamentoAtual.Conexao
{
    
        class Conexao
        {
            private String connectionString;

            public String getConnectionString()
            {
                connectionString = ConfigurationManager.ConnectionStrings
                ["EstacionamentoAtual.Properties.Settings.estacionamentoConnectionString"].ConnectionString;
                return connectionString;
            }
        }
    
}
