namespace HishabNikash.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message, int statusCode = 404) : base(message, statusCode) { }
    }
}
