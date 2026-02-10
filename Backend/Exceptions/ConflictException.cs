using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Exceptions
{
    public class ConflictException : AppException
    {
        public ConflictException(string message) : base(message)
        {
        }
    }
}