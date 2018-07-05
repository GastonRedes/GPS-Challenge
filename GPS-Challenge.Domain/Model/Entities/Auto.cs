using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class Auto : ITipoVehiculo
    {
        
        public string Nombre()
        {
            return "Auto";
        }
        
        public bool PiquetePermitePasar()
        {
            return false;
        }

        public bool ControlPolicialPermitePasar()
        {
            return true;
        }

        public bool PozoPermitePasar()
        {
            return true;
        }

        public int MovimientosDeObstaculoPiquete()
        {
            return 1;
        }
        
        public int MovimientosDeObstaculoPozo()
        {
            return 3;
        }
        
        public int MovimientosDeObstaculoControlPolicial()
        {
            Random random = new Random();

            // genero numero aleatorio entre 0 y 9

            if (random.Next(10) < 5)

                return 3;

            return 0;
        }
        
        public ITipoVehiculo CambioVehiculo()
        {
            return new CuatroPorCuatro();
        }
    }
}
