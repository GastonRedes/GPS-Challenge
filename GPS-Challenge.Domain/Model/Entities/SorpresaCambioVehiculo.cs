using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class SorpresaCambioVehiculo : Sorpresa
    {
        public override void Accionar(Vehiculo vehiculo)
        {
            vehiculo.Tipo = vehiculo.Tipo.CambioVehiculo();
        }
    }
}
