using System;

namespace HotelInfoServer.Exceptions
{
    //custom model validation rule exception type
    public class HotelUriInvalidException : Exception
    {
        public HotelUriInvalidException() { }
        public HotelUriInvalidException(string message) : base(message) { }
        public HotelUriInvalidException(string message, Exception inner) : base(message, inner) { }
        protected HotelUriInvalidException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
