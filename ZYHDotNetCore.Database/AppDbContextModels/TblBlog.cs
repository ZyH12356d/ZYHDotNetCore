using System;
using System.Collections.Generic;

namespace ZYHDotNetCore.Database.AppDbContextModels;

public partial class TblBlog
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? ContentData { get; set; }

    public byte DeleteFlag { get; set; }
}
