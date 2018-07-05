using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Challenge.Domain.Model.Entities
{
    public interface ITipoVehiculo
    {
        string Nombre();
        
        int MovimientosDeObstaculoPozo();
        int MovimientosDeObstaculoControlPolicial();
        int MovimientosDeObstaculoPiquete();

        bool PozoPermitePasar();
        bool ControlPolicialPermitePasar();
        bool PiquetePermitePasar();
        
        ITipoVehiculo CambioVehiculo();
    }
}
