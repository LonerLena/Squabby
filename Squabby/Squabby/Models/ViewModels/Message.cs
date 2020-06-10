namespace Squabby.Models.ViewModels
{
    public class Message 
    {
        public MessageType Type { get; set; }
        public string MessageString { get; set; }
        public string Description { get; set; }
    }

    public enum MessageType 
    {
       Success, Warning, Error
    }
}