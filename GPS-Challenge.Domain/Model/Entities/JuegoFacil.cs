using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class JuegoFacil : Juego
    {
        public JuegoFacil( Jugador jugador ) : base( "Facil" , 80 , jugador )
        {
        }

        public override int Puntos()
        {
            return MovimientosSobrantes();
        }
    }
}
