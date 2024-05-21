using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class Feedback
{
    public string FeedbackId { get; set; } = null!;

    public int? GenreId { get; set; }

    public int? DisscountId { get; set; }

    public int RateStar { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? Status { get; set; }

    public string? Content { get; set; }

    public int? ProductId { get; set; }

    public virtual Disscount? Disscount { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual Product? Product { get; set; }
}
