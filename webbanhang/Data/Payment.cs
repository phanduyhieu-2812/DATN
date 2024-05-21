using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string? PaymentName { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public string? Status { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateAt { get; set; }
}
