using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class OderDetail
{
    public double? Price { get; set; }

    public string? Status { get; set; }

    public int? Quantity { get; set; }

    public string? Transection { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int? OrderId { get; set; }

    public int OderDetailId { get; set; }

    public string? Size { get; set; }

    public string? AccountId { get; set; }

    public int? ProductId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
