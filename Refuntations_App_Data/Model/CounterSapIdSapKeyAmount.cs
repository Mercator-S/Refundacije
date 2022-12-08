using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string? k_uns { set; get; }
        public DateTime? d_upd { set; get; }
        public bool? active { set; get; }
        public int? mesec { get; set; }
        public int? godina { get; set; }
    }
}
