using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class OrderAddress
{
    public int OrderAddressId { get; set; }

    public string? OrderPhonenumber { get; set; }

    public string? OrderUsername { get; set; }

    public string? Content { get; set; }

    public int? Timeedit { get; set; }

    public int ProvinceId { get; set; }

    public int WardId { get; set; }

    public int DistrictId { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual Province Province { get; set; } = null!;

    public virtual Ward Ward { get; set; } = null!;
}
