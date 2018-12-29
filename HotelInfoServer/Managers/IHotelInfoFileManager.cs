using HotelInfoServer.Models;
using Microsoft.AspNetCore.Http;

namespace HotelInfoServer.Managers
{
    public interface IHotelInfoFileManager
    {
        //writes input file and  generates outputs files
        void ProcessFile(IFormCollection form);
        HotelInfo[] ParseHotelInfoToList(IFormCollection form);
        void ValidateForm(IFormCollection form);

    }
}
