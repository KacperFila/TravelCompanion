using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentValidation;
using FluentValidation.Results;
using TravelCompanion.Shared.Abstractions.Exceptions;
using Humanizer;

namespace TravelCompanion.Shared.Infrastructure.Exceptions
{
    internal class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new();
        
        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                TravelCompanionException ex => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message))
                    , HttpStatusCode.BadRequest),
                ValidationException ex => new ExceptionResponse(
                    new ErrorsResponse(GetValidationFailureErrors(ex))
                    , HttpStatusCode.BadRequest),

                _ => new ExceptionResponse(new ErrorsResponse(new Error("error", "There was an error.")),
                    HttpStatusCode.InternalServerError)
            };

        private record Error(string Code, string Message);

        private record ErrorsResponse(params Error[] Errors);

        private static string GetErrorCode(object exception)
        {
            var type = exception.GetType();
            return Codes.GetOrAdd(type, type.Name.Underscore().Replace("_exception", string.Empty));
        }

        private static Error[] GetValidationFailureErrors(ValidationException exception)
        {
            return exception.Errors.Select(error => new Error(error.ErrorCode, error.ErrorMessage)).ToArray();
        }
    }
}