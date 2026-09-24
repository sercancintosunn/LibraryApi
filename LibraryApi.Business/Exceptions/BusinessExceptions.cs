namespace LibraryApi.Business.Exceptions
{
    public sealed class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Email veya şifre hatalı") { }
    }

    public sealed class DuplicateEmailException : Exception
    {
        public DuplicateEmailException() : base("Bu email ile zaten biri kayıtlı") { }
    }

    public sealed class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message) { }
    }
}
