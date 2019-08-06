using System;
using System.Runtime.Serialization;

namespace TheSustainables.VendingMachine.Domain.Exceptions
{
    [Serializable]
    public class NotEnoughCreditException : Exception
    {
        public NotEnoughCreditException()
        {
        }

        public NotEnoughCreditException(string message) : base(message)
        {
        }

        public NotEnoughCreditException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughCreditException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}