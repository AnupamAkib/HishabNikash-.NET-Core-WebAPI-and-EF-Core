namespace HishabNikash.Exceptions
{
    public class InvalidInputException : CustomException
    {
        public InvalidInputException(string message, int statusCode = 400) : base(message, statusCode) { }
    }
}
