using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MediMitra.Services
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            Console.WriteLine("senderId");
            Console.WriteLine($"senderId : {senderId}");
            Console.WriteLine($"receiverId  : {receiverId}");
            Console.WriteLine($"message  : {message}");
            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId) || string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("SenderId, ReceiverId, and Message cannot be null or empty.");
            }
            await Clients.User(receiverId).SendAsync("ReceiveMessage", senderId, message);
            Console.WriteLine($"Message sent from {senderId} to {receiverId}: {message}");
            //save to database yesss tala
        }
    }
}
