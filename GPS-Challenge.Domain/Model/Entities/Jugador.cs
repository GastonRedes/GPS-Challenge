using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GPS_Challenge.Domain.Model.Exceptions;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class Jugador
    {
        private string nombre;

        private Vehiculo vehiculo;


        public Jugador( string nombre , Vehiculo vehiculo )
        {
            if ( nombre.Trim() == "" )

                throw new NombreInvalidoException( "Nombre inválido" );

            this.nombre = nombre.Trim();

            this.vehiculo = vehiculo;
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
        }

        public Vehiculo Vehiculo
        {
            get
            {
                return vehiculo;
            }
        }
    }
}
