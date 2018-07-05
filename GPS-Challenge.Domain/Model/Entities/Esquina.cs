using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GPS_Challenge.Domain.Model.Exceptions;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class Esquina
    {
        /**************************************************************************
         * Las representaciones de esquina y cuadras segun vectores se da como sigue:
         * Segun subindices:
         * 0: Norte.
         * 1: Sur.
         * 2: Este.
         * 3: Oeste.
         * *************************************************************************/

        private Esquina[] esquinas;
        private Cuadra[] cuadras;
        private bool esLlegada;

        public Esquina() 
        {
            esquinas = new Esquina[4];
            cuadras = new Cuadra[4];
        }

        public Esquina GetEsquina(Orientacion orient)
        {
            return this.esquinas[(int)orient];
        }

        public Cuadra GetCuadra(Orientacion orient) 
        {
            return this.cuadras[(int)orient];
        }

        public void SetEsquina(Orientacion orient, Esquina esq) 
        {
            this.esquinas[(int)orient] = esq;
        }

        public void SetCuadra(Orientacion orient, Cuadra cuad) 
        {
            this.cuadras[(int)orient] = cuad;
        }

        public void SetLlegada()
        {
            esLlegada = true;
        }
        public bool EsLlegada()
        {
            return esLlegada;
        }

        public virtual bool EsMeta()
        {
            return false;
        }



       
    }
}
