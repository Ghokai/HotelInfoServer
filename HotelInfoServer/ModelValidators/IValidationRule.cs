namespace HotelInfoServer.ModelValidators
{
    public interface IValidationRule<T>
    {
        bool ValidateModel(T t);
    }
}
