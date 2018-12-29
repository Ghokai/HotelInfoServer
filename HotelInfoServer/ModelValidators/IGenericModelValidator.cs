namespace HotelInfoServer.ModelValidators
{
    public interface IGenericModelValidator<T> : IValidationRule<T>
    {
        //loads rules dynamically for generic type
        void LoadValidationRules(string ConfigurationValidationRulesName, string ConfigurationDelimiter);
    }
}
