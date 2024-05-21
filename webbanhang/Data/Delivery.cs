using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class Delivery
{
    public int DeliveryId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? Status { get; set; }
}
