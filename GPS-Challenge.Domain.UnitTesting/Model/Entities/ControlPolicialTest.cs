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
    public class ControlPolicialTest
    {
        [Test]
        public void SiPenalizaAutoDespuesDeIntentarVariasVecesYPermiteMover() 
        {
            Vehiculo auto = Vehiculo.Auto();

            Obstaculo controlPolicial = new ControlPolicial();
            
            while ( auto.Movimientos == 0 )
                
                controlPolicial.Penalizar(auto);
            
            Assert.IsTrue( auto.Movimientos == 3 );

            Assert.IsTrue( controlPolicial.PermiteMover( auto ) );
        }

        [Test]
        public void SiPenalizaMotoDespuesDeIntentarVariasVecesYPermiteMover()
        {
            Vehiculo moto = Vehiculo.Moto();

            Obstaculo controlPolicial = new ControlPolicial();

            while ( moto.Movimientos == 0 )

                controlPolicial.Penalizar(moto);

            Assert.IsTrue( moto.Movimientos == 3 );

            Assert.IsTrue( controlPolicial.PermiteMover( moto ) );
        }

        [Test]
        public void SiPenalizaCuatroPorCuatroDespuesDeIntentarVariasVecesYPermiteMover()
        {
            Vehiculo cuatroPorCuatro = Vehiculo.CuatroPorCuatro();

            Obstaculo controlPolicial = new ControlPolicial();

            while ( cuatroPorCuatro.Movimientos == 0 )

                controlPolicial.Penalizar(cuatroPorCuatro);

            Assert.IsTrue( cuatroPorCuatro.Movimientos == 3 );

            Assert.IsTrue( controlPolicial.PermiteMover( cuatroPorCuatro ) );
        }
    }
}
