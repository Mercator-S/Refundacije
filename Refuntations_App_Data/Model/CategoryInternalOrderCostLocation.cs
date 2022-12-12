using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refuntations_App_Data.Model
{
    [Table("tab_refundacije_sif_Kategorija_InterniNalog_MestoTroska")]
    public class CategoryInternalOrderCostLocation
    {
        [Key]
        public int id { get; set; }
        public string? sifra_kat { get; set; }
        public string? naziv_kat { get; set; }
        public string? interni_nalog { get; set; }
        public string mesto_troska { get; set; }
        public string? k_ins { set; get; }
        public DateTime? d_ins { set; get; }
        public string? k_upd { set; get; }
        public DateTime? d_upd { set; get; }
        public bool? active { set; get; }
    }
}
