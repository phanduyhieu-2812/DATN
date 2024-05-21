using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class ReplyFeedback
{
    public int ReplyFeedbackId { get; set; }

    public int FeedbackId { get; set; }

    public string AccountId { get; set; } = null!;

    public string? Content { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual Account Account { get; set; } = null!;
}
