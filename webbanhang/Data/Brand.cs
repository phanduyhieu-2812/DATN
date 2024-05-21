using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class Brand
{
    public int BrandId { get; set; }

    public string? BrandName { get; set; }

    public DateTime? CreateAt { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
