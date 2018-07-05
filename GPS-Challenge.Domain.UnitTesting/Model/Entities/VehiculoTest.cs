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
    public class VehiculoTest
    {
        

        [Test]
        public void MoverVehiculoDeUnaEsquinaAOtra()
        {
            Esquina esquina = new Esquina();
            Esquina esquinaOeste = new Esquina();
            Cuadra cuadraOeste = new Cuadra(null,null);
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.Esquina = esquina;

            Assert.True(vehiculo.Esquina == esquina);

            esquina.SetEsquina(Orientacion.Oeste, esquinaOeste);
            esquina.SetCuadra(Orientacion.Oeste, cuadraOeste);
            vehiculo.MoverVehiculo(Orientacion.Oeste);
            
            Assert.True(vehiculo.Esquina == esquinaOeste);
        }

        [Test]
        public void MoverVehiculoDeUnaEsquinaAOtraAumentaMovimiento()
        {
            Esquina esquina = new Esquina();
            Esquina esquinaOeste = new Esquina();
            Cuadra cuadraOeste = new Cuadra(null,null);
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.Esquina = esquina;

            esquina.SetEsquina(Orientacion.Oeste, esquinaOeste);
            esquina.SetCuadra(Orientacion.Oeste, cuadraOeste);
            
            Assert.True(vehiculo.Movimientos == 0);

            vehiculo.MoverVehiculo(Orientacion.Oeste);

            Assert.True(vehiculo.Movimientos == 1);
        }

        [Test]
        public void MoverVehiculoAutoDeUnaEsquinaAOtraConPozo()
        {
            Esquina esquina = new Esquina();
            Esquina esquinaOeste = new Esquina();
            Vehiculo vehiculo = Vehiculo.Auto();

            vehiculo.Esquina = esquina;
            Pozo pozo = new Pozo();
            Cuadra cuadraOeste = new Cuadra(null,pozo);

            esquina.SetEsquina(Orientacion.Oeste, esquinaOeste);
            esquina.SetCuadra(Orientacion.Oeste, cuadraOeste);

            Assert.True(vehiculo.Movimientos == 0);
            vehiculo.MoverVehiculo(Orientacion.Oeste);

            Assert.True(vehiculo.Movimientos == 4);

        }

        [Test]
        public void MoverVehiculoAutoDeUnaEsquinaAOtraConSorpresaCambioVehiculo()
        {
            Esquina esquina = new Esquina();
            Esquina esquinaOeste = new Esquina();
            Vehiculo vehiculo = Vehiculo.Auto();

            vehiculo.Esquina = esquina;
            Sorpresa sorpresa = new SorpresaCambioVehiculo();
            Cuadra cuadraOeste = new Cuadra(sorpresa, null);

            esquina.SetEsquina(Orientacion.Oeste, esquinaOeste);
            esquina.SetCuadra(Orientacion.Oeste, cuadraOeste);

            Assert.True(vehiculo.Tipo is Auto);
            vehiculo.MoverVehiculo(Orientacion.Oeste);

            Assert.True(vehiculo.Tipo is CuatroPorCuatro);
        }

        [Test]
        public void MoverVehiculoAEsquinaInexistenteDaException()
        {
            Esquina esquina = new Esquina();
            Vehiculo vehiculo = new Vehiculo();
            
            vehiculo.Esquina = esquina;

            Assert.Throws<EsquinaInexistenteException>(() => vehiculo.MoverVehiculo(Orientacion.Norte));
        }

        [Test]
        public void MoverVehiculoDeEsquinaSinVehiculoDaException()
        {
            Vehiculo vehiculo = new Vehiculo();

            Assert.Throws<VehiculoNoPosicionadoInexistenteException>(() => vehiculo.MoverVehiculo(Orientacion.Norte));
        }
    }
}
