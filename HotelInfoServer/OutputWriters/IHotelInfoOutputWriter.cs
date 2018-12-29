using HotelInfoServer.Models;

namespace HotelInfoServer.OutputWriters
{
    public interface IHotelInfoOutputWriter
    {
        void WriteToOutputFile(string filePath, HotelInfo[] hotelInfos);
        string GenerateOutputContent(HotelInfo[] hotelInfos);
    }
}
