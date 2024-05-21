using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class Product
{
    public int BrandId { get; set; }

    public int GenreId { get; set; }

    public string? ProductName { get; set; }

    public int? Price { get; set; }

    public long? Views { get; set; }

    public long? Byturn { get; set; }

    public double? Quantity { get; set; }

    public string? Status { get; set; }

    public string? Type { get; set; }

    public string? Specifications { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int ProductId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<OderDetail> OderDetails { get; set; } = new List<OderDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
