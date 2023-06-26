using System;
using System.Collections.Generic;

namespace AviaSea.DateBase;

public partial class AllPost
{
    public int? IdUser { get; set; }

    public byte[]? ImagePosts { get; set; }

    public string? Info { get; set; }

    public int? KolvoSee { get; set; }

    public int IdPosts { get; set; }

    public string? Name { get; set; }

    public string? Family { get; set; }

    public byte[]? Img { get; set; }

    public DateTime? Date { get; set; }
}
