using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TalkNTalk.Models;

namespace TalkNTalk.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private TalkNtalkContext _dbContext { get; }
    private readonly string _botUser;

    public ChatHub(TalkNtalkContext context)
    {
        _dbContext = context;
        _botUser = "MyChat Bot";
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {

        Connection? connection =  _dbContext.Connections.Where(c => c.ConnectionId == Context.ConnectionId).FirstOrDefault();
        if (connection is not null)
        {
            _dbContext.Connections.Remove(connection);
            _dbContext.SaveChanges();
            Clients.Group(connection.ChatRoom).SendAsync("ReceiveMessage", _botUser, $"{connection.UserName} has left");
            SendUsersConnected(connection.ChatRoom);
        }

        return base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Joining the room
    /// </summary>
    /// <param name="userConnection"></param>
    /// <returns></returns>
    public async Task JoinRoom(UserConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.ChatRoom);

        Connection connection = new Connection
        {
            ConnectionId = Context.ConnectionId,
            ChatRoom = userConnection.ChatRoom,
            UserName = userConnection.UserName,
        };
        await _dbContext.AddAsync(connection);
        await _dbContext.SaveChangesAsync();

        await Clients.Group(userConnection.ChatRoom).SendAsync("ReceiveMessage", _botUser, $"{userConnection.UserName} has joined {userConnection.ChatRoom}");

        await SendUsersConnected(userConnection.ChatRoom);
    }

    /// <summary>
    /// Send messagein chat room
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessage(string message)
    {
        Connection? connection = await _dbContext.Connections.Where(c => c.ConnectionId == Context.ConnectionId).FirstOrDefaultAsync();
        if (connection is not null)
        {
            await Clients.Group(connection.ChatRoom).SendAsync("ReceiveMessage", connection.UserName, message);
        }
    }

    /// <summary>
    /// Get all the users connected to the chat room
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    public Task SendUsersConnected(string room)
    {
        var users =  _dbContext.Connections.Where(c => c.ChatRoom == room).Select(c => c.UserName).ToList();
        return Clients.Group(room).SendAsync("UsersInRoom", users);
    }
}
