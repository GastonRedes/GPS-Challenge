using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class Piquete : Obstaculo
    {
        public override void Penalizar(Vehiculo vehiculo)
        {
            vehiculo.AgregarMovimientos(vehiculo.Tipo.MovimientosDeObstaculoPiquete());
        }

        public override bool PermiteMover(Vehiculo vehiculo)
        {
            return vehiculo.Tipo.PiquetePermitePasar();
        }
    }
}
