using System;
using System.Collections.Generic;

namespace ZYHDotNetCore.Database.AppDbContextModels;

public partial class TblTransactionHistory
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public string FromNumber { get; set; } = null!;

    public string ToNumber { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Type { get; set; } = null!;
}
