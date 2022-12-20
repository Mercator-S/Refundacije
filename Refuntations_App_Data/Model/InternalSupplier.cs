using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Refuntations_App_Data.Model
{
    [Table("tab_refundacije_sif_interni_dobavljaci")]
    public class InternalSupplier
    {
        [Key]
        public int id { get; set; }
        public int? sifra_int_dobavljac { get; set; }
        public string? naziv_int_dobavljac { set; get; }
        public string? k_ins { set; get; }
        public DateTime? d_ins { set; get; }
        public string? k_upd { set; get; }
        public DateTime? d_upd { set; get; }
        public bool? active { set; get; }

    }
}
