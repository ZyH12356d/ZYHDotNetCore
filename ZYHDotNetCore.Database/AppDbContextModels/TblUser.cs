using System;
using System.Collections.Generic;

namespace ZYHDotNetCore.Database.AppDbContextModels;

public partial class TblUser
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int Pin { get; set; }

    public byte DeleteFlag { get; set; }

    public int Balance { get; set; }
}
