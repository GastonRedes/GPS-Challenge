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
    public class JugadorTest
    {
        [Test]
        public void CreoJugador()
        {
            Jugador jugador = new Jugador( "Juan" , Vehiculo.Auto() );

            Assert.IsTrue( jugador.Nombre == "Juan" );

            Assert.IsTrue( jugador.Vehiculo is Vehiculo );
        }

        [Test]
        public void CreoJugadorConNombreInvalido()
        {
            Assert.Throws<NombreInvalidoException>( () => new Jugador( "" , Vehiculo.Auto() ) );

            Assert.Throws<NombreInvalidoException>( () => new Jugador( "    " , Vehiculo.Auto() ) );
        }
    }
}
