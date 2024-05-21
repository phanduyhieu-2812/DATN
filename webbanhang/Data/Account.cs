using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class Account
{
    public string AccountId { get; set; } = null!;

    public string? Password { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? Status { get; set; }

    public string? Email { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Avatar { get; set; }

    public string? RandomKey { get; set; }

    public string? Province { get; set; }

    public string? Ward { get; set; }

    public string? Districs { get; set; }

    public string? Address { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<AccountAddress> AccountAddresses { get; set; } = new List<AccountAddress>();

    public virtual ICollection<OderDetail> OderDetails { get; set; } = new List<OderDetail>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ReplyFeedback> ReplyFeedbacks { get; set; } = new List<ReplyFeedback>();
}
