using HotelInfoServer.Exceptions;
using HotelInfoServer.Models;
using System;
using System.Text;

namespace HotelInfoServer.ModelValidators
{
    public class HotelNameValidator : IValidationRule<HotelInfo>
    {
        //hotel name validation logic implementation
        public bool ValidateModel(HotelInfo hInfo)
        {
            Byte[] bytes = Encoding.UTF8.GetBytes(hInfo.Name);
            string nameUTF8 = Encoding.UTF8.GetString(bytes);

            if (!hInfo.Name.Equals(nameUTF8))
            {
                throw new HotelNameNotUTF8Exception($"Hotel Name({hInfo.Name}) is not UTF-8!");
            }
            return true;
        }
    }
}
