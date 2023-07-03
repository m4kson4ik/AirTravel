using System;
using System.Collections.Generic;

namespace AviaSea;

public partial class LikedAviaSea
{
    public int? IdUser { get; set; }

    public string LikedToken { get; set; } = null!;

    public int IdLiked { get; set; }
}
