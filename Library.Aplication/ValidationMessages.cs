namespace Library.Aplication
{
    public static class ValidationMessages
    {
        public static string PropertyIsRequired(string propertyName) => $"{propertyName} is required!";
        public static string PropertyMustNotBeEmpty(string propertyName) => $"{propertyName} must not be empty!";
        public static string PropertyHasInvalidLenght(string propertyName) => $"{propertyName} has invalid lenght!";

        public static string PropertyHasInvalidValue(string propertyName) => $"{propertyName} has invalid value!";
    }
}
