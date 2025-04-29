using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class SendMessageRequest
    {
        public string RoomId { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
    }
}
