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
    public class PiqueteTest
    {
        [Test]
        public void PenalizarMotoAgrega2Movimientos() 
        {
            Vehiculo moto = Vehiculo.Moto();
            Obstaculo piquete = new Piquete();
            Esquina actual = new Esquina(), siguiente = new Esquina();

            moto.Esquina = actual;

            piquete.Penalizar(moto);

            Assert.IsTrue(moto.Movimientos == 2);
        }

        [Test]
        public void PenalizarMotoLaDejaPasar()
        {
            Vehiculo moto = Vehiculo.Moto();
            Obstaculo piquete = new Piquete();

            piquete.Penalizar(moto);

            Assert.IsTrue(moto.Movimientos == 2);
            Assert.IsTrue(piquete.PermiteMover(moto));
        }

        [Test]
        public void PenalizarAutoNoLoDejaPasar()
        {
            Vehiculo auto = Vehiculo.Auto();
            Obstaculo piquete = new Piquete();

            piquete.Penalizar(auto);

            Assert.IsTrue(auto.Movimientos == 1);
            Assert.IsFalse(piquete.PermiteMover(auto));
        }

        [Test]
        public void PenalizarCuatroPorCuatroNoLaDejaPasar()
        {
            Vehiculo cuatroXcuatro = Vehiculo.CuatroPorCuatro();
            Piquete piquete = new Piquete();

            piquete.Penalizar(cuatroXcuatro);

            Assert.IsTrue(cuatroXcuatro.Movimientos == 1);
            Assert.IsFalse(piquete.PermiteMover(cuatroXcuatro));
        }
    }
}
