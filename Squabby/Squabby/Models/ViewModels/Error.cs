namespace Squabby.Models.ViewModels
{
    public class Error : Message
    {
        public Error(ErrorType errorType, string errorMessage = null)
        {
            Type = MessageType.Error;
            ErrorType = errorType;
            MessageString = errorMessage;
        }

        public ErrorType ErrorType { get; set; }
    }

    public enum ErrorType
    {
        NameAlreadyUsedError, LoginError,
        Unknown
    }
}