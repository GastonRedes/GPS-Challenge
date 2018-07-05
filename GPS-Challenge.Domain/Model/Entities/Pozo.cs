using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class Pozo : Obstaculo
    {
        public override void Penalizar(Vehiculo vehiculo)
        {
            vehiculo.AgregarMovimientos(vehiculo.Tipo.MovimientosDeObstaculoPozo());
        }

        public override bool PermiteMover(Vehiculo vehiculo)
        {
            return vehiculo.Tipo.PozoPermitePasar();
        }
    }
}
