using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class SorpresaFavorable : Sorpresa
    {
        private double descuento;

        public SorpresaFavorable()
        {
            this.descuento = 0.2;
        }

        public override void Accionar(Vehiculo vehiculo)
        {
            vehiculo.RestarMovimientos( (int)( vehiculo.Movimientos * this.descuento) );
        }
    }
}
