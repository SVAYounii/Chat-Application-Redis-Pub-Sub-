using Domain.DTO;
using Domain.Interface.Chat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace To_Do_Redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
        {
            await _chatService.CreateRoomAsync(request.RoomId);
            return Ok();
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinRoom([FromBody] JoinRoomRequest request)
        {
            bool exists = await _chatService.RoomExistsAsync(request.RoomId);
            if (!exists)
                return NotFound("Room does not exist.");

            return Ok();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            bool exists = await _chatService.RoomExistsAsync(request.RoomId);
            if (!exists)
                return NotFound("Room does not exist.");

            await _chatService.SendMessageAsync(request.RoomId, request.Sender, request.Message);
            return Ok();
        }

        [HttpGet("history/{roomId}")]
        public async Task<IActionResult> GetChatHistory(string roomId)
        {
            var history = await _chatService.GetChatHistoryAsync(roomId);
            return Ok(history);
        }
    }
}
