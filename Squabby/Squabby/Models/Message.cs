namespace Squabby.Models
{
    public class Message 
    {
        public Message(MessageType type, string messageString = null)
        {
            Type = type;
            MessageString = messageString;
        }
        
        public MessageType Type;
        public string MessageString;
    }

    public enum MessageType 
    {
       Success, Warning, Error,
       
       RegisterError, LoginError
    }
}