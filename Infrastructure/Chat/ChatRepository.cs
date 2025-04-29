using Domain.DTO;
using Domain.Interface.Chat;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Chat
{
    public class ChatRepository : IChatRepository
    {
        private readonly IDatabase _db;
        private readonly ISubscriber _subscriber;

        public ChatRepository(IConnectionMultiplexer redis)
        {
            _db = redis.GetDatabase();
            _subscriber = redis.GetSubscriber();
        }

        public async Task SetAddAsync(string key, string value)
        {
            await _db.SetAddAsync(key, value);
        }

        public async Task<bool> SetContainsAsync(string key, string value)
        {
            return await _db.SetContainsAsync(key, value);
        }

        public async Task StreamAddAsync(string key, NameValueEntry[] entries)
        {
            await _db.StreamAddAsync(key, entries);
        }

        public async Task<StreamEntry[]> StreamReadAsync(string key, string fromId = "0-0")
        {
            return await _db.StreamReadAsync(key, fromId);
        }

        public async Task PublishAsync(string channel, string message)
        {
            await _subscriber.PublishAsync(channel, message);
        }
    }


}
