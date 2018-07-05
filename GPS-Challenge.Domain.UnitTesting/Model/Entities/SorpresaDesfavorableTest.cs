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
    public class SorpresaDesfavorableTest
    {
        [Test]
        public void AccionarReduceEl20PorCientoDeLosMovimientosDeMoto() 
        {
            Vehiculo vehiculo = Vehiculo.Moto();
            Sorpresa sorpresa = new SorpresaDesfavorable();

            vehiculo.AgregarMovimientos(4);
            
            sorpresa.Accionar(vehiculo);

            Assert.IsTrue(vehiculo.Movimientos == 5);
        }

        [Test]
        public void AccionarReduceEl20PorCientoDeLosMovimientosDeAuto()
        {
            Vehiculo vehiculo = Vehiculo.Auto();
            Sorpresa sorpresa = new SorpresaDesfavorable();

            vehiculo.AgregarMovimientos(4);

            sorpresa.Accionar(vehiculo);

            Assert.IsTrue(vehiculo.Movimientos == 5);
        }

        [Test]
        public void AccionarReduceEl20PorCientoDeLosMovimientosDeCuatroXCuatro()
        {
            Vehiculo vehiculo = Vehiculo.CuatroPorCuatro();
            Sorpresa sorpresa = new SorpresaDesfavorable();

            vehiculo.AgregarMovimientos(4);

            sorpresa.Accionar(vehiculo);

            Assert.IsTrue(vehiculo.Movimientos == 5);
        }
    }
}
