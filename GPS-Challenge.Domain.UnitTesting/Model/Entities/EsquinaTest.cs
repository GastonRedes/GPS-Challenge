using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using GPS_Challenge.Domain.Model.Entities;
using GPS_Challenge.Domain.Model.Exceptions;

namespace GPS_Challenge.Domain.UnitTesting.Model.Entities
{
    [TestFixture]
    public class EsquinaTest
    {
        [Test]
        public void VecinosIncializanEnNUll()
        {
            Esquina esquina = new Esquina();

            Assert.IsNull(esquina.GetEsquina(Orientacion.Norte));
            Assert.IsNull(esquina.GetEsquina(Orientacion.Sur));
            Assert.IsNull(esquina.GetEsquina(Orientacion.Este));
            Assert.IsNull(esquina.GetEsquina(Orientacion.Oeste));
        }

        [Test]
        public void AgregarEsquinaNorte() 
        { 
            Esquina esquina = new Esquina();
            Esquina esquinaNorte = new Esquina();
            esquina.SetEsquina(Orientacion.Norte, esquinaNorte);

            Assert.IsTrue(esquina.GetEsquina(Orientacion.Norte) == esquinaNorte);
        }

        [Test]
        public void AgregarEsquinaSur()
        {
            Esquina esquina = new Esquina();
            Esquina esquinaSur = new Esquina();
            esquina.SetEsquina(Orientacion.Sur, esquinaSur);

            Assert.IsTrue(esquina.GetEsquina(Orientacion.Sur) == esquinaSur);
        }

        [Test]
        public void AgregarEsquinaEste()
        {
            Esquina esquina = new Esquina();
            Esquina esquinaEste = new Esquina();
            esquina.SetEsquina(Orientacion.Este, esquinaEste);

            Assert.IsTrue(esquina.GetEsquina(Orientacion.Este) == esquinaEste);
        }

        [Test]
        public void AgregarEsquinaOeste()
        {
            Esquina esquina = new Esquina();
            Esquina esquinaOeste = new Esquina();
            esquina.SetEsquina(Orientacion.Oeste, esquinaOeste);

            Assert.IsTrue(esquina.GetEsquina(Orientacion.Oeste) == esquinaOeste);
        }

        [Test]
        public void EsMetaDevuelveFalse()
        {
            Esquina esquina = new Esquina();

            Assert.IsFalse(esquina.EsMeta());
        }
        [Test]
        public void AgregarCuadras() 
        {
            Esquina esquina = new Esquina();
            
            Cuadra cuadraNorte = new Cuadra(null,null);
            Cuadra cuadraSur = new Cuadra(null,null);
            Cuadra cuadraEste = new Cuadra(null,null);
            Cuadra cuadraOeste = new Cuadra(null,null);
            
            esquina.SetCuadra(Orientacion.Norte, cuadraNorte);
            esquina.SetCuadra(Orientacion.Sur, cuadraSur);
            esquina.SetCuadra(Orientacion.Este, cuadraEste);
            esquina.SetCuadra(Orientacion.Oeste, cuadraOeste);

            Assert.IsTrue(esquina.GetCuadra(Orientacion.Norte) == cuadraNorte);
            Assert.IsTrue(esquina.GetCuadra(Orientacion.Sur) == cuadraSur);
            Assert.IsTrue(esquina.GetCuadra(Orientacion.Este) == cuadraEste);
            Assert.IsTrue(esquina.GetCuadra(Orientacion.Oeste) == cuadraOeste);
        }
    }
}
