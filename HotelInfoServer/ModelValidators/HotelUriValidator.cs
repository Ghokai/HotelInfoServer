using HotelInfoServer.Exceptions;
using HotelInfoServer.Models;
using System;

namespace HotelInfoServer.ModelValidators
{
    public class HotelUriValidator : IValidationRule<HotelInfo>
    {
        //hotel uri validation logic implementation
        public bool ValidateModel(HotelInfo hInfo)
        {

            if (!Uri.IsWellFormedUriString(hInfo.Uri, UriKind.Absolute))
            {
                throw new HotelUriInvalidException($"Hotel({hInfo.Name})  Uri({hInfo.Uri})  is not valid!");
            }
            else
            {
                return true;
            }
        }
    }
}
