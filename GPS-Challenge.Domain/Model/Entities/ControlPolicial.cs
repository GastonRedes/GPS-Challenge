using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class ControlPolicial : Obstaculo
    {
        public override void Penalizar(Vehiculo vehiculo)
        {
            vehiculo.AgregarMovimientos(vehiculo.Tipo.MovimientosDeObstaculoControlPolicial());
        }

        public override bool PermiteMover(Vehiculo vehiculo)
        {
            return vehiculo.Tipo.ControlPolicialPermitePasar();
        }

    }
}