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
    public class CuadraTest
    {
        [Test]
        public void CuadraInicializaSinSorpresaNiObstaculo()
        {
            Cuadra cuadra = new Cuadra(null,null);

            Assert.IsNull(cuadra.Sorpresa);
            Assert.IsNull(cuadra.Obstaculo);
        }

        [Test]
        public void AgregarSorpresa() 
        { 
            SorpresaFavorable sorpresa = new SorpresaFavorable();
            Cuadra cuadra = new Cuadra(sorpresa,null);

            Assert.IsTrue(cuadra.Sorpresa is SorpresaFavorable);
        }

        [Test]
        public void AgregarObstaculo() 
        {
            Pozo pozo = new Pozo();
            Cuadra cuadra = new Cuadra(null,pozo);

            Assert.IsTrue(cuadra.Obstaculo is Pozo);
        }
    }
}
