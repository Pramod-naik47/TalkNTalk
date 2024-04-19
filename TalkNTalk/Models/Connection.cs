using System;
using System.Collections.Generic;

namespace TalkNTalk.Models;

public partial class Connection
{
    public int Id { get; set; }

    public string? ConnectionId { get; set; }

    public string ChatRoom { get; set; } = null!;

    public string? UserName { get; set; }
}
