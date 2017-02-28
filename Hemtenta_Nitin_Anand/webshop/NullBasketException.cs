using System;
using System.Runtime.Serialization;

namespace HemtentaTdd2017.webshop
{
    [Serializable]
    public class NullBasketException : Exception
    {
        public NullBasketException()
        {
        }

        public NullBasketException(string message) : base(message)
        {
        }

        public NullBasketException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NullBasketException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}