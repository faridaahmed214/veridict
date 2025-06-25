using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DomanLayer.Exceptions
{
    public sealed class BadRequestdException(List<string> errors) : Exception(message: "Validation Failed")
    {
     public List<string> Errors { get; } = errors;
    }
}
