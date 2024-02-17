using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Users.Core.Exceptions
{
    internal class InvalidCredentialsException : TravelCompanionException
    {
        public InvalidCredentialsException() : base("Invalid credentials.")
        {
        }
    }
}
