using System;
using System.Runtime.Serialization;

namespace TheSustainables.VendingMachine.Domain.Exceptions
{
    [Serializable]
    public class UnacceptableReturnAmmountException : Exception
    {
        public UnacceptableReturnAmmountException()
        {
        }

        public UnacceptableReturnAmmountException(string message) : base(message)
        {
        }

        public UnacceptableReturnAmmountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnacceptableReturnAmmountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}