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
    public class SorpresaCambioVehiculoTest
    {

        [Test]
        public void AutoCambiaACuatroPorCuatro()
        {
            Vehiculo vehiculo = Vehiculo.Auto();
            Sorpresa cambioVehiculo = new SorpresaCambioVehiculo();

            Assert.IsTrue(vehiculo.Tipo is Auto);
            cambioVehiculo.Accionar(vehiculo);
            Assert.IsTrue(vehiculo.Tipo is CuatroPorCuatro);
        }

        [Test]
        public void CuatroPorCuatroCambiaAMoto()
        {
            Vehiculo vehiculo = Vehiculo.CuatroPorCuatro();
            Sorpresa cambioVehiculo = new SorpresaCambioVehiculo();

            Assert.IsTrue(vehiculo.Tipo is CuatroPorCuatro);
            cambioVehiculo.Accionar(vehiculo);
            Assert.IsTrue(vehiculo.Tipo is Moto);
        }

        [Test]
        public void MotoCambiaAAuto()
        {
            Vehiculo vehiculo = Vehiculo.Moto();
            Sorpresa cambioVehiculo = new SorpresaCambioVehiculo();

            Assert.IsTrue(vehiculo.Tipo is Moto);
            cambioVehiculo.Accionar(vehiculo);
            Assert.IsTrue(vehiculo.Tipo is Auto);
        }

    }
}
