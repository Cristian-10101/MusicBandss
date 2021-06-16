using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicBands.Models
{
    public class MusicBand
    {
        [Key]
        public int BandId { get; set; }

        [Display (Name ="Nombre")]
        public string BandName { get; set; }
        [Display(Name = "Género")]
        public string BandGenero { get; set; }
        [Display(Name = "N° Álbumes ")]
        public int CantAlbum { get; set; }

    }
}
