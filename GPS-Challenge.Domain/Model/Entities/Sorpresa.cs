using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Challenge.Domain.Model.Entities
{
    public abstract class Sorpresa
    {
        public abstract void Accionar(Vehiculo vehiculo);
    }
}
