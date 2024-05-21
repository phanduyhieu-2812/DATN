using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class Genre
{
    public int GenreId { get; set; }

    public string? GenreName { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? Icon { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
