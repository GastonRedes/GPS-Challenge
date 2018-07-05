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
    public class AutoTest
    {
        [Test]
        public void CreoAuto()
        {
            int movimientos;

            ITipoVehiculo auto = new Auto();

            Assert.IsTrue( auto.Nombre() == "Auto" );
            
            Assert.IsFalse( auto.PiquetePermitePasar() );

            Assert.IsTrue( auto.ControlPolicialPermitePasar() );

            Assert.IsTrue( auto.PozoPermitePasar() );

            Assert.IsTrue( auto.MovimientosDeObstaculoPiquete() == 1 );

            Assert.IsTrue( auto.MovimientosDeObstaculoPozo() == 3 );

            movimientos = 3;

            while (movimientos == 3)
            {
                movimientos = auto.MovimientosDeObstaculoControlPolicial();
            }

            Assert.IsTrue(movimientos == 0);

            Assert.IsTrue( auto.CambioVehiculo() is CuatroPorCuatro );
        }

    }
}
