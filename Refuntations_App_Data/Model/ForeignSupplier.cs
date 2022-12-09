using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refuntations_App_Data.Model
{
    [Table("tab_refundacije_sif_inO_dobavljaci")]
    public class ForeignSupplier
    {
        [Key]
        public int id { get; set; }
        public int? sifra_ino_dobavljac { get; set; }
        public string? naziv_ino_dobavljac { set; get; }
        public string? k_ins { set; get; }
        public DateTime? d_ins { set; get; }
        public string? k_upd { set; get; }
        public DateTime? d_upd { set; get; }
        public bool? active { set; get; }
    }
}
