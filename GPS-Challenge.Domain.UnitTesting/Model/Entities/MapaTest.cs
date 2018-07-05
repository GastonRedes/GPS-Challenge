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
    public class MapaTest
    {
        [Test]
        public void ChequearVecinos() 
        {
            Mapa mapa = new Mapa(10);

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    if (i > 0)
                        Assert.IsTrue(mapa.GetEsquina(i, j).GetEsquina(Orientacion.Norte) == mapa.GetEsquina(i - 1, j));
                    if (i < 9)
                        Assert.IsTrue(mapa.GetEsquina(i, j).GetEsquina(Orientacion.Sur) == mapa.GetEsquina(i + 1, j));
                    if (j < 9)
                        Assert.IsTrue(mapa.GetEsquina(i, j).GetEsquina(Orientacion.Este) == mapa.GetEsquina(i, j + 1));
                    if (j > 0)
                        Assert.IsTrue(mapa.GetEsquina(i, j).GetEsquina(Orientacion.Oeste) == mapa.GetEsquina(i, j - 1));
                }
        }
        
        [Test]
        public void ChequearCuadrasDeVecinos() 
        {
            Mapa mapa = new Mapa(10);
           
            for (int i = 0; i < 10 - 1; i++)
            {
                for (int j = 0; j < 10 - 1; j++)
                {
                    Assert.IsTrue(mapa.GetEsquina(i, j).GetCuadra(Orientacion.Este) == mapa.GetEsquina(i, j + 1).GetCuadra(Orientacion.Oeste));

                    Assert.IsTrue(mapa.GetEsquina(i, j).GetCuadra(Orientacion.Sur) == mapa.GetEsquina(i + 1, j).GetCuadra(Orientacion.Norte));
                }
            }

            for (int i = 0; i < 10 - 1; i++)
            {
                Assert.IsTrue(mapa.GetEsquina(9, i).GetCuadra(Orientacion.Este) == mapa.GetEsquina(9, i + 1).GetCuadra(Orientacion.Oeste));

                Assert.IsTrue(mapa.GetEsquina(i, 9).GetCuadra(Orientacion.Sur) == mapa.GetEsquina(i + 1, 9).GetCuadra(Orientacion.Norte));
            }       
        }

        [Test]
        public void PedirEsquinaFueraDeRangoLanzaException()
        {
            Mapa mapa = new Mapa(10);

            Assert.Throws<PosicionFueraDeRangoException>(() => mapa.GetEsquina(0, 10));
            Assert.Throws<PosicionFueraDeRangoException>(() => mapa.GetEsquina(10, 0));
        }

        [Test]
        public void ChequeaTamanio()
        {
            Mapa mapa = new Mapa(10);

            Assert.IsTrue(mapa.GetTamanio() == 10);
        }
    }
}
