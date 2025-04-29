using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Chat
{
    public interface IChatService
    {
        Task CreateRoomAsync(string roomId);
        Task<List<ChatMessage>> GetChatHistoryAsync(string roomId);
        Task<bool> RoomExistsAsync(string roomId);
        Task SendMessageAsync(string roomId, string sender, string message);
    }
}
