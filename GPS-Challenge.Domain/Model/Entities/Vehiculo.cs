using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPS_Challenge.Domain.Model.Exceptions;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class Vehiculo
    {
        private int movimientos;
        private ITipoVehiculo tipo;

        public Vehiculo()
        {
            this.movimientos = 0;
        }

        public static Vehiculo Moto()
        {
            Vehiculo moto = new Vehiculo();
            moto.Tipo = new Moto();
            return moto;
        }

        public static Vehiculo Auto()
        {
            Vehiculo auto = new Vehiculo();
            auto.Tipo = new Auto();
            return auto;
        }

        public static Vehiculo CuatroPorCuatro()
        {
            Vehiculo cuatroPorCuatro = new Vehiculo();
            cuatroPorCuatro.Tipo = new CuatroPorCuatro();
            return cuatroPorCuatro;
        }

        public ITipoVehiculo Tipo
        {
            get { return this.tipo; }
            set { this.tipo = value; }
        }

        public int Movimientos
        {
            get { return this.movimientos; }
        }

        public void AgregarMovimientos(int cantidad) {
            this.movimientos += cantidad;
        }

        public void RestarMovimientos(int cantidad) {
            this.movimientos -= cantidad;
        }
        
        public Esquina Esquina { get; set; }

        public void MoverVehiculo(Orientacion orient)
        {
            if (this.Esquina == null)
                throw new VehiculoNoPosicionadoInexistenteException();
            
            if (this.Esquina.GetEsquina(orient) == null)
                throw new EsquinaInexistenteException();
            
            bool sePuedeMover = true;
            this.movimientos++;
            Obstaculo obstaculo = this.Esquina.GetCuadra(orient).Obstaculo;

            if ( obstaculo != null)
            {
                obstaculo.Penalizar(this);
                sePuedeMover = obstaculo.PermiteMover(this);
            }

            if (this.Esquina.GetCuadra(orient).Sorpresa != null)
                this.Esquina.GetCuadra(orient).Sorpresa.Accionar(this);

            if ( sePuedeMover )
                this.Esquina = this.Esquina.GetEsquina(orient);
        }
    }
}