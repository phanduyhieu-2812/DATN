using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class Order
{
    public string AccountId { get; set; } = null!;

    public double? Total { get; set; }

    public string Status { get; set; } = null!;

    public string? OrderNote { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? Phonenumber { get; set; }

    public string? Province { get; set; }

    public string? Ward { get; set; }

    public string? District { get; set; }

    public string? Address { get; set; }

    public string? Delivery { get; set; }

    public string? Payment { get; set; }

    public string? Name { get; set; }

    public int? DisscountId { get; set; }

    public int OrderId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Disscount? Disscount { get; set; }

    public virtual ICollection<OderDetail> OderDetails { get; set; } = new List<OderDetail>();
}
