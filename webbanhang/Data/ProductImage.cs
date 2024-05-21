using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class ProductImage
{
    public int GenreId { get; set; }

    public int DisscountId { get; set; }

    public int ProductImgId { get; set; }

    public string? Images { get; set; }

    public int? ProductId { get; set; }

    public virtual Disscount Disscount { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
