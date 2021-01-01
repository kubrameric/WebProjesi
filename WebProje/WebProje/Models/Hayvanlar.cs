using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProje.Models
{
    public class Hayvanlar
    {
        [Key]
        public int hayvanId { get; set; }
        public int hayvanTuruId { get; set; }//0 köpek 1 kedi demek
        public int hayvanYasi { get; set; }
        public string hayvanCinsi { get; set; }
        public string hayvanCinsiyeti { get; set; }
        public virtual Ilanlar Ilanlar { get; set; }

    }
}
