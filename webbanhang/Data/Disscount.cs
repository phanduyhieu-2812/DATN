using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class Disscount
{
    public int DisscountId { get; set; }

    public string? DisscountName { get; set; }

    public DateTime? DisscountStart { get; set; }

    public DateTime? DisscountEnd { get; set; }

    public int DisscountPrice { get; set; }

    public string? DisscountCode { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
