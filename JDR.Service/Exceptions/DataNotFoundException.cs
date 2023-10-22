using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDR.Core.Exceptions
{
    public class DataNotFoundException : ApplicationException
    {
        public DataNotFoundException(string? message) : base(message)
        {
        }
    }
}
