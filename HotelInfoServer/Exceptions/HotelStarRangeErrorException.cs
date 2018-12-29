using System;

namespace HotelInfoServer.Exceptions
{
    //custom model validation rule exception type
    public class HotelStarRangeErrorException : Exception
    {
        public HotelStarRangeErrorException() { }
        public HotelStarRangeErrorException(string message) : base(message) { }
        public HotelStarRangeErrorException(string message, Exception inner) : base(message, inner) { }
        protected HotelStarRangeErrorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
