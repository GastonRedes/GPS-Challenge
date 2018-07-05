using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS_Challenge.Domain.Model.Entities
{
    public abstract class Obstaculo
    {
        public abstract void Penalizar(Vehiculo vehiculo);

        public abstract bool PermiteMover(Vehiculo vehiculo);
    }
}
