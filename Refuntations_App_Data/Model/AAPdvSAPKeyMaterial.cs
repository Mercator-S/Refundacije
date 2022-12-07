using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refuntations_App_Data.Model
{
    [Table("tab_refundacije_sif_AA_PDV_SAPKljuc_Materijal_NEW")]
    public class AAPdvSAPKeyMaterial
    {
        [Key]
        public int id { get; set; }
        public int? sifra_aa { get; set; }
        public string? naziv_aa { get; set; }
        public decimal? PDV { get; set; }
        public string? SAP_Kljuc { get; set; }
        public string? Materijal { get; set; }
        public string? k_ins { set; get; }
        public DateTime? d_ins { set; get; }
        public string? k_uns { set; get; }
        public DateTime? d_upd { set; get; }
        public bool? active { set; get; }
    }
}
