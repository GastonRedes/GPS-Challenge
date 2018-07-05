using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Challenge.Domain.Model.Entities
{
    public class Cuadra
    {
        private Sorpresa sorpresa;
        private Obstaculo obstaculo;

        public Cuadra(Sorpresa sorParametro, Obstaculo obsParametro)
        {
            this.sorpresa = sorParametro;
            this.obstaculo = obsParametro;
        }

        public Sorpresa Sorpresa 
        {
            get { return this.sorpresa; }
        }
        
        public Obstaculo Obstaculo 
        {
            get { return this.obstaculo; }
        }
    }
}
