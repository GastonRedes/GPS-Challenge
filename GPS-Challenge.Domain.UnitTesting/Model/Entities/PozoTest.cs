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
    class PozoTest
    {

        [Test]
        public void PenalizarMotoAgrega3MovimientosYPermiteMover()
        {
            Vehiculo moto = Vehiculo.Moto();
            Obstaculo pozo = new Pozo();

            pozo.Penalizar(moto);

            Assert.IsTrue(moto.Movimientos == 3);
            Assert.IsTrue(pozo.PermiteMover(moto));
        }

        [Test]
        public void PenalizarAutoAgrega3MovimientosYPermiteMover()
        {
            Vehiculo auto = Vehiculo.Auto();
            Obstaculo pozo = new Pozo();

            pozo.Penalizar(auto);

            Assert.IsTrue(auto.Movimientos == 3);
            Assert.IsTrue(pozo.PermiteMover(auto));
        }

        [Test]
        public void PenalizarCuatroXCuatroNoAgregaMovimientosYPermiteMover()
        {
            Vehiculo cuatroXcuatro = Vehiculo.CuatroPorCuatro();
            Obstaculo pozo = new Pozo();

            pozo.Penalizar(cuatroXcuatro);

            Assert.IsTrue(cuatroXcuatro.Movimientos == 0);
            Assert.IsTrue(pozo.PermiteMover(cuatroXcuatro));
        }
    }
}
