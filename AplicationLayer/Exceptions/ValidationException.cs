using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLayer.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() :base("Error de validación."){}
        public ValidationException(string error) : base(error) { }
    }
}
