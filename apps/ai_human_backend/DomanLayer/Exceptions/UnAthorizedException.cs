using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Exceptions
{
    public sealed class UnAthorizedException(string Massage="Invalid Email Or Password") : Exception(Massage)
    {   
    }
}
