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
    public class JuegoTest
    {
        [Test]
        public void CreoJuegoEnModoFacil()
        {
            Jugador jugador = new Jugador( "Juan" , Vehiculo.Auto() );

            Juego juego = new JuegoFacil( jugador );

            Assert.IsTrue( juego.Modo == "Facil" );

            Assert.IsTrue( juego.LimiteDeMovimientos == 80 );

            Assert.IsTrue( juego.MovimientosSobrantes() == 80 );

            Assert.IsTrue(juego.NombreDelVehiculo() == "Auto");

            Assert.IsTrue( juego.Puntos() == 80 );

            Assert.IsTrue(juego.Jugador == jugador);

            Assert.IsTrue(juego.Mapa is Mapa);

            Assert.IsTrue(juego.GetLlegadaX() == 8);

            Assert.IsTrue(juego.GetLlegadaY() == 10);

            Assert.IsFalse(juego.JuegoTermino());

            Assert.IsFalse(juego.JugadorGano());
        }

        [Test]
        public void CreoJuegoEnModoModerado()
        {
            Jugador jugador = new Jugador( "Juan" , Vehiculo.CuatroPorCuatro() );

            Juego juego = new JuegoModerado( jugador );

            Assert.IsTrue( juego.Modo == "Moderado" );

            Assert.IsTrue( juego.LimiteDeMovimientos == 60 );

            Assert.IsTrue( juego.MovimientosSobrantes() == 60 );

            Assert.IsTrue(juego.NombreDelVehiculo() == "4x4");

            Assert.IsTrue( juego.Puntos() == 120 );
        }

        [Test]
        public void CreoJuegoEnModoDificil()
        {
            Jugador jugador = new Jugador( "Juan" , Vehiculo.Moto() );

            Juego juego = new JuegoDificil( jugador );

            Assert.IsTrue( juego.Modo == "Dificil" );

            Assert.IsTrue( juego.LimiteDeMovimientos == 50 );

            Assert.IsTrue( juego.MovimientosSobrantes() == 50 );

            Assert.IsTrue(juego.NombreDelVehiculo() == "Moto");

            Assert.IsTrue( juego.Puntos() == 150 );
        }

        [Test]
        public void SiJuegoTerminaGanando()
        {
            Jugador jugador = new Jugador("Juan", Vehiculo.Auto());

            Juego juego = new JuegoModerado(jugador);

            juego.Jugador.Vehiculo.Esquina.SetLlegada();

            Assert.IsTrue(juego.JuegoTermino());

            Assert.IsTrue(juego.JugadorGano());
        }

        [Test]
        public void SiJuegoTerminaPerdiendo()
        {
            Jugador jugador = new Jugador("Juan", Vehiculo.Auto());

            Juego juego = new JuegoModerado(jugador);

            juego.Jugador.Vehiculo.AgregarMovimientos(60);

            Assert.IsTrue(juego.JuegoTermino());
        }
    }
}
