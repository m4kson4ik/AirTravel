using System;
using System.Collections.Generic;

namespace AviaSea;

public partial class Post
{
    public int? IdUser { get; set; }

    public byte[]? ImagePosts { get; set; }

    public string? Info { get; set; }

    public int? KolvoSee { get; set; }

    public int IdPosts { get; set; }
}
