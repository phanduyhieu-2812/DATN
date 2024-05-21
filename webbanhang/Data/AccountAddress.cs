using System;
using System.Collections.Generic;

namespace webbanhang.Data;

public partial class AccountAddress
{
    public int AccountAddressId { get; set; }

    public string? AccountPhonenumber { get; set; }

    public string? AccountUsername { get; set; }

    public string? Content { get; set; }

    public bool? Isdefault { get; set; }

    public int ProvinceId { get; set; }

    public int WardId { get; set; }

    public int DistrictId { get; set; }

    public string AccountId { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual District District { get; set; } = null!;

    public virtual Province Province { get; set; } = null!;

    public virtual Ward Ward { get; set; } = null!;
}
