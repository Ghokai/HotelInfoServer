using HotelInfoServer.Exceptions;
using HotelInfoServer.Models;

namespace HotelInfoServer.ModelValidators
{
    public class HotelRatingValidator : IValidationRule<HotelInfo>
    {
        //hotel rating validation logic implementation
        public bool ValidateModel(HotelInfo hInfo)
        {
            if (hInfo.Stars > 5 || hInfo.Stars < 1)
            {
                throw new HotelStarRangeErrorException($"Hotel({hInfo.Name})  Stars({hInfo.Stars})  are not in range!");
            }
            else
            {
                return true;
            }
        }
    }
}
