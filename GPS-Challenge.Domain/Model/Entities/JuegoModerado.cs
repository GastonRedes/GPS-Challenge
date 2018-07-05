using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class JuegoModerado : Juego
    {
        public JuegoModerado( Jugador jugador ) : base( "Moderado" , 60 , jugador )
        {
        }

        public override int Puntos()
        {
            return MovimientosSobrantes() * 2;
        }
    }
}
