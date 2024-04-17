using System;
using System.Collections.Generic;

namespace TalkNTalk.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
}
