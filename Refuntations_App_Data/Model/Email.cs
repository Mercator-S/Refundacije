using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Refuntations_App_Data.Model
{
    [Table("tab_refundacije_sif_dobavljaci_mail")]
    public class Email
    {
        [Key]
        public int id { get; set; }
        public int? sifra { set; get; }
        public string? sap_sifra { set; get; }
        public string? naziv { set; get; }
        public string? mail { set; get; }
        public string? k_ins { set; get; }
        public DateTime? d_ins { set; get; }
        public string? k_upd { set; get; }
        public DateTime? d_upd { set; get; }
        public bool? active { set; get; }
    }
}
