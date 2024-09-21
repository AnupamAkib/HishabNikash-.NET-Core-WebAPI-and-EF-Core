namespace HishabNikash.Exceptions
{
    public class AlreadyExistException : CustomException
    {
        public AlreadyExistException(string message, int statusCode = 409) : base(message, statusCode) { }
    }
}
