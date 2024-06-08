using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace TodoBot.API.Todo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly IHubContext<ChatHub> _hubContext;

    public TodoController(ILogger<TodoController> logger,
                          IHubContext<ChatHub> hubContext)
    {
        _logger = logger;
        this._hubContext = hubContext;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok ();
    }

    [HttpPost]
    public IActionResult Post()
    {
        this._hubContext.Clients.All.SendAsync ("Notify", "service", "message");
        return Ok ();
    }
}
