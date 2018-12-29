using HotelInfoServer.AssemblyLoaders;

namespace HotelInfoServer.ModelValidators
{
    public class GenericModelValidator<T> : IGenericModelValidator<T>
    {
        private IDynamicLoader _dynamicLoader;
        private IValidationRule<T>[] _validationList;

        //injects constructor parameter by configured services
        public GenericModelValidator(IDynamicLoader dynamicLoader)
        {
            this._dynamicLoader = dynamicLoader;
        }

        //load validation rules dynamically at runtime from configuration
        public void LoadValidationRules(string ConfigurationValidationRulesName, string ConfigurationDelimiter)
        {
            _validationList = this._dynamicLoader.LoadAssembliesDynamicallyFromConfig<IValidationRule<T>>(ConfigurationValidationRulesName, ConfigurationDelimiter);
        }

        //validate with loaded rules
        public bool ValidateModel(T t)
        {
            foreach (IValidationRule<T> rule in _validationList)
            {
                rule.ValidateModel(t);
            }
            return true;
        }
    }
}
