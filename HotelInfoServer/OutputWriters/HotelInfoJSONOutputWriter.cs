using HotelInfoServer.Models;
using Newtonsoft.Json;

namespace HotelInfoServer.OutputWriters
{
    public class HotelInfoJSONOutputWriter: HotelInfoOutputWriterBase
    {
        public HotelInfoJSONOutputWriter() : base("json")
        {

        }

        //overrides to abstract method for imlementing format spesific logic
        public override string GenerateOutputContent(HotelInfo[] hotelInfos)
        {
            string jsonStr = JsonConvert.SerializeObject(hotelInfos);
            return jsonStr;
        }
    }
}
