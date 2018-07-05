using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Challenge.Domain.Model.Exceptions
{
    public class NombreInvalidoException : ApplicationException
    {
        public NombreInvalidoException( string mensaje ) : base( mensaje )
        {
        }
    }
}
