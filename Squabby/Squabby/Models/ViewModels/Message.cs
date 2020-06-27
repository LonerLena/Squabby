namespace Squabby.Models.ViewModels
{
    public class Message
    {
        public Message()
        {
            ButtonText = "Go Back";
            ButtonHref = "javascript:history.back()";
        }

        public MessageType Type { get; set; }
        public string MessageString { get; set; }
        public string Description { get; set; }

        public string ButtonText { get; set; }
        public string ButtonHref { get; set; }
    }

    public enum MessageType
    {
        Success,
        Warning,
        Error
    }
}