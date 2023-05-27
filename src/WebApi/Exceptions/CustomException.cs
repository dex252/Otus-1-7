using System;
using WebApi.Enums;

namespace WebApi.Exceptions
{
    public class CustomException: Exception
    {
        public ErrorType ErrorType { get; }
        public CustomException(ErrorType errorType, string message = null) :base(message)
        {
            ErrorType = errorType;
        }
    }
}
