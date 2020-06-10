using Microsoft.AspNetCore.Mvc;
using Squabby.Models.ViewModels;

namespace Squabby.Helpers
{
    public static class MessageHelper
    {
        public static ViewResult Message(this Controller controller, string messageString, string description = null)
            => controller.View("~/Views/Home/customMessage.cshtml",new Message {MessageString = messageString, Description = description});
    }
}