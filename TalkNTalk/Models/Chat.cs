using System;
using System.Collections.Generic;

namespace TalkNTalk.Models;

public partial class Chat
{
    public int ChatId { get; set; }

    public string? Message { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
