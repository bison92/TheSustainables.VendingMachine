using System;
using System.Runtime.Serialization;

namespace TheSustainables.VendingMachine.Domain.Exceptions
{
    [Serializable]
    public class UnacceptableReturnAmountException : Exception
    {
        public UnacceptableReturnAmountException()
        {
        }

        public UnacceptableReturnAmountException(string message) : base(message)
        {
        }

        public UnacceptableReturnAmountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnacceptableReturnAmountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}