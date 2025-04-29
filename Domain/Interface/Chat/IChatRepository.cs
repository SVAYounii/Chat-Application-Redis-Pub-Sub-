using Domain.DTO;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Chat
{
    public interface IChatRepository
    {
        Task SetAddAsync(string key, string value);
        Task<bool> SetContainsAsync(string key, string value);
        Task StreamAddAsync(string key, NameValueEntry[] entries);
        Task<StreamEntry[]> StreamReadAsync(string key, string fromId = "0-0");
        Task PublishAsync(string channel, string message);
    }

}
