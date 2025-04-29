using Domain.DTO;
using Domain.Interface.Chat;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Chat
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task CreateRoomAsync(string roomId)
        {
            var key = "chat:rooms";

            if (!await _chatRepository.SetContainsAsync(key, roomId))
            {
                await _chatRepository.SetAddAsync(key, roomId);
            }
            else
            {
                throw new InvalidOperationException("Room already exists.");
            }
        }


        public async Task<List<ChatMessage>> GetChatHistoryAsync(string roomId)
        {
            string streamKey = $"chat_stream:{roomId}";

            var entries = await _chatRepository.StreamReadAsync(streamKey, "0-0");

            return entries.Select(entry => new ChatMessage
            {
                Sender = entry["sender"],
                Message = entry["message"],
                Timestamp = DateTime.Parse(entry["timestamp"])
            }).ToList();
        }

        public async Task<bool> RoomExistsAsync(string roomId)
        {
            string setKey = "chat:rooms";

            return await _chatRepository.SetContainsAsync(setKey, roomId);
        }

        public async Task SendMessageAsync(string roomId, string sender, string message)
        {
            string setKey = "chat:rooms";
            string streamKey = $"chat_stream:{roomId}";
            string pubSubChannel = $"chat:{roomId}";

            if (!await _chatRepository.SetContainsAsync(setKey, roomId))
            {
                throw new InvalidOperationException("Room does not exist.");
            }

            var payload = new
            {
                sender,
                message,
                timestamp = DateTime.UtcNow.ToString("o")
            };
            string payloadJson = JsonSerializer.Serialize(payload);

            // Bericht opslaan in stream
            await _chatRepository.StreamAddAsync(streamKey, new NameValueEntry[]
            {
        new NameValueEntry("sender", sender),
        new NameValueEntry("message", message),
        new NameValueEntry("timestamp", DateTime.UtcNow.ToString("o"))
            });

            // Bericht publiceren via Pub/Sub
            await _chatRepository.PublishAsync(pubSubChannel, payloadJson);
        }

    }
}
