using System;

namespace HotelInfoServer.Exceptions
{
    //custom model validation rule exception type
    public class HotelNameNotUTF8Exception : Exception
    {
        public HotelNameNotUTF8Exception() { }
        public HotelNameNotUTF8Exception(string message) : base(message) { }
        public HotelNameNotUTF8Exception(string message, Exception inner) : base(message, inner) { }
        protected HotelNameNotUTF8Exception(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
