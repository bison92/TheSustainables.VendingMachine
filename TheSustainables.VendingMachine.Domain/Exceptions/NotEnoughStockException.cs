using System;
using System.Runtime.Serialization;

namespace TheSustainables.VendingMachine.Domain.Exceptions
{
    [Serializable]
    public class NotEnoughStockException : Exception
    {
        public NotEnoughStockException()
        {
        }

        public NotEnoughStockException(string message) : base(message)
        {
        }

        public NotEnoughStockException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}