using System;
using System.Collections.Generic;

namespace DALWithEF.Models;

public partial class TblMovie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public int GenreId { get; set; }

    public virtual TblGenre Genre { get; set; } = null!;
}
