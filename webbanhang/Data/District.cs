using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class District
{
    public int DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public string? Type { get; set; }

    public int ProvinceId { get; set; }

    public virtual ICollection<AccountAddress> AccountAddresses { get; set; } = new List<AccountAddress>();

    public virtual ICollection<OrderAddress> OrderAddresses { get; set; } = new List<OrderAddress>();

    public virtual Province Province { get; set; } = null!;

    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}
