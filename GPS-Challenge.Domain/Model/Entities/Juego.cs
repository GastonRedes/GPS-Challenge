using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS_Challenge.Domain.Model.Entities
{
    public abstract class Juego
    {
        private string modo;
        
        private int limiteDeMovimientos;

        private Jugador jugador;

        private Mapa mapa;

        private int llegadaX;

        private int llegadaY;
        
        
        public Juego( string modo , int limiteDeMovimientos , Jugador jugador )
        {
            this.modo = modo;

            this.limiteDeMovimientos = limiteDeMovimientos;

            this.jugador = jugador;
            
            mapa = new Mapa(11);
            
            jugador.Vehiculo.Esquina = mapa.GetEsquina(3, 0);
            
            llegadaX = 8;

            llegadaY = 10;
            
            mapa.GetEsquina(GetLlegadaX(), GetLlegadaY()).SetLlegada();
        }

        public string Modo
        {
            get { return modo; }
        }

        public int LimiteDeMovimientos
        {
            get { return limiteDeMovimientos; }
        }

        public Jugador Jugador
        {
            get { return jugador; }
        }

        public Mapa Mapa
        {
            get { return mapa; }
        }

        public int MovimientosSobrantes()
        {
            return limiteDeMovimientos - jugador.Vehiculo.Movimientos;
        }

        public string NombreDelVehiculo()
        {
            return jugador.Vehiculo.Tipo.Nombre();
        }

        public abstract int Puntos();

        public int GetLlegadaX()
        {
            return llegadaX;
        }

        public int GetLlegadaY()
        {
            return llegadaY;
        }

        public bool JuegoTermino()
        {
            if (MovimientosSobrantes() <= 0 || JugadorGano())
                return true;
            return false;
        }

        public bool JugadorGano()
        {
            if (jugador.Vehiculo.Esquina.EsLlegada())
                return true;
            return false;
        }
    }
}
