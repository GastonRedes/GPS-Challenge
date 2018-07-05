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
    public class MotoTest
    {
        [Test]
        public void CreoMoto()
        {
            int movimientos;

            ITipoVehiculo moto = new Moto();

            Assert.IsTrue( moto.Nombre() == "Moto" );

            Assert.IsTrue( moto.PiquetePermitePasar() );

            Assert.IsTrue( moto.ControlPolicialPermitePasar() );

            Assert.IsTrue( moto.PozoPermitePasar() );

            Assert.IsTrue( moto.MovimientosDeObstaculoPiquete() == 2 );

            Assert.IsTrue( moto.MovimientosDeObstaculoPozo() == 3 );

            movimientos = 3;

            while (movimientos == 3)
            {
                movimientos = moto.MovimientosDeObstaculoControlPolicial();
            }

            Assert.IsTrue(movimientos == 0);

            Assert.IsTrue( moto.CambioVehiculo() is Auto );
        }
    }
}
