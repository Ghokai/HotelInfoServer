using HotelInfoServer.Models;
using System.Xml.Serialization;

namespace HotelInfoServer.OutputWriters
{
    public class HotelInfoXMLOutputWriter : HotelInfoOutputWriterBase
    {
        public HotelInfoXMLOutputWriter() : base("xml")
        {

        }

        //overrides to abstract method for imlementing format spesific logic
        public override string GenerateOutputContent(HotelInfo[] hotelInfos)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(HotelInfo[]));

            var stringwriter = new System.IO.StringWriter();
            serializer.Serialize(stringwriter, hotelInfos);
            return stringwriter.ToString();
        }
    }
}
