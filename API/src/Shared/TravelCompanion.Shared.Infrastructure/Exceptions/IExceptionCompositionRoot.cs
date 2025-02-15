using System;
using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Shared.Infrastructure.Exceptions
{
    internal interface IExceptionCompositionRoot
    {
        ExceptionResponse Map(Exception exception);
    }
}