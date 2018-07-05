using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using GPS_Challenge.Domain.Model.Entities;

namespace GPS_Challenge.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class CuatroPorCuatroTest
    {
        [Test]
        public void CreoCuatroPorCuatro()
        {
            int movimientos;

            ITipoVehiculo cuatroPorCuatro = new CuatroPorCuatro();

            Assert.IsTrue( cuatroPorCuatro.Nombre() == "4x4" );

            Assert.IsFalse( cuatroPorCuatro.PiquetePermitePasar() );

            Assert.IsTrue( cuatroPorCuatro.ControlPolicialPermitePasar() );

            Assert.IsTrue( cuatroPorCuatro.PozoPermitePasar() );

            Assert.IsTrue( cuatroPorCuatro.MovimientosDeObstaculoPiquete() == 1 );

            Assert.IsTrue( cuatroPorCuatro.MovimientosDeObstaculoPozo() == 0 );
            
            movimientos = 3;

            while (movimientos == 3)
            {
                movimientos = cuatroPorCuatro.MovimientosDeObstaculoControlPolicial();
            }

            Assert.IsTrue(movimientos == 0);

            Assert.IsTrue( cuatroPorCuatro.CambioVehiculo() is Moto );
        }
    }
}
