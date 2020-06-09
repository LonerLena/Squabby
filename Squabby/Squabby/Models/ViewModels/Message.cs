namespace Squabby.Models.ViewModels
{
    public class Message 
    {
        public Message(MessageType type, string messageString = null, string description = null)
        {
            Type = type;
            MessageString = messageString;
            Description = description;
        }

        public MessageType Type { get; set; }
        public string MessageString { get; set; }
        public string Description { get; set; }
    }

    public enum MessageType 
    {
       Success, Warning, Error,
       RegisterError, LoginError
    }
}