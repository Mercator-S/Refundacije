using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Refuntations_App_Data.Model
{
    [Table("tab_refundacije_sif_Brojac_SAPkljuc")]
    public class CounterSapIdSapKeyAmount
    {
        [Key]
        public int id { get; set; }
        public long brojac { get; set; }
        public string? SAP_sifra_dobavljac { set; get; }
        public string? br_knjiznog_zaduzenja { set; get; }
        public string? SAP_kljuc { set; get; }
        public double? iznos { get; set; }
        public string? k_ins { set; get; }
        public DateTime? d_ins { set; get; }
        public string? k_upd { set; get; }
        public DateTime? d_upd { set; get; }
        public bool? active { set; get; }
        public int? mesec { get; set; }
        public int? godina { get; set; }
    }
}
