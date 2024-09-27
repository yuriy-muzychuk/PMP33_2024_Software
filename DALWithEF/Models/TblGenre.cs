using System;
using System.Collections.Generic;

namespace DALWithEF.Models;

public partial class TblGenre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TblMovie> TblMovies { get; set; } = new List<TblMovie>();
}
