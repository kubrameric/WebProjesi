using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProje.Models
{
    public class Ilanlar
    {
        [Key]
        public int ilanID { get; set; }
        public int ilanNo { get; set; }
        public int hayvanId { get; set; }

        public DateTime ilanTarihi { get; set; }
        [ForeignKey("hayvanId")]
        public virtual Hayvanlar hayvan { get; set; }
    }
}
