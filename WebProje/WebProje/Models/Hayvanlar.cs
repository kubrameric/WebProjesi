using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProje.Models
{
    public class Hayvanlar
    {
        [Key]
        public int hayvanId { get; set; }
        [DisplayName("Hayvan Türü")]
        public int hayvanTuruId { get; set; }//0 köpek 1 kedi demek
        [DisplayName("Hayvan Yaşı")]
        public int hayvanYasi { get; set; }
        [DisplayName("Hayvan Cinsi")]
        public string hayvanCinsi { get; set; }
        [DisplayName("Hayvan Cinsiyeti")]
        public string hayvanCinsiyeti { get; set; }
        public virtual Ilanlar Ilanlar { get; set; }

    }
}
