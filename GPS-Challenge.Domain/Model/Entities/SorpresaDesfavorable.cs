using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class SorpresaDesfavorable : Sorpresa
    {
        private double recarga;

        public SorpresaDesfavorable()
        {
            this.recarga = 0.25;
        }

        public override void Accionar(Vehiculo vehiculo)
        {
            vehiculo.AgregarMovimientos( (int)( vehiculo.Movimientos * this.recarga) );
        }
    }
}
