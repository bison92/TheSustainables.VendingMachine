using System;
using System.Runtime.Serialization;

namespace TheSustainables.VendingMachine.Domain.Exceptions
{
    [Serializable]
    public class UnknownProductIdException : Exception
    {
        public UnknownProductIdException()
        {
        }

        public UnknownProductIdException(string message) : base(message)
        {
        }

        public UnknownProductIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnknownProductIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}