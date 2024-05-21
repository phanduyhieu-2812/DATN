using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class Ward
{
    public int WardId { get; set; }

    public string? WardName { get; set; }

    public string? Type { get; set; }

    public int DistrictId { get; set; }

    public virtual ICollection<AccountAddress> AccountAddresses { get; set; } = new List<AccountAddress>();

    public virtual District District { get; set; } = null!;

    public virtual ICollection<OrderAddress> OrderAddresses { get; set; } = new List<OrderAddress>();
}
