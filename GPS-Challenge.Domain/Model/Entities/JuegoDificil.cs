using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class JuegoDificil : Juego
    {
        public JuegoDificil( Jugador jugador ) : base( "Dificil" , 50 , jugador )
        {
        }

        public override int Puntos()
        {
            return MovimientosSobrantes() * 3;
        }
    }
}
