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
    public class SorpresaFavorableTest
    {
        [Test]
        public void AccionarReduceEl20PorCientoDeLosMovimientosDeMoto() 
        {
            Vehiculo moto = Vehiculo.Moto();
            Sorpresa sorpresa = new SorpresaFavorable();

            moto.AgregarMovimientos(5);

            sorpresa.Accionar(moto);

            Assert.IsTrue(moto.Movimientos == 4);
        }
        
        [Test]
        public void AccionarReduceEl20PorCientoDeLosMovimientosDeAuto()
        {
            Vehiculo vehiculo = Vehiculo.Auto();
            Sorpresa sorpresa = new SorpresaFavorable();

            vehiculo.AgregarMovimientos(5);
            
            sorpresa.Accionar(vehiculo);

            Assert.IsTrue(vehiculo.Movimientos == 4);
        }

        [Test]
        public void AccionarReduceEl20PorCientoDeLosMovimientosDeCuatroXCuatro()
        {
            Vehiculo vehiculo = Vehiculo.CuatroPorCuatro();
            Sorpresa sorpresa = new SorpresaFavorable();

            vehiculo.AgregarMovimientos(5);

            sorpresa.Accionar(vehiculo);

            Assert.IsTrue(vehiculo.Movimientos == 4);
        }
    }
}
