using System.ComponentModel.DataAnnotations.Schema;

namespace Refuntations_App_Data.Model
{
    [Table("tab_refundacije_odobrenja_statusi")]
    public class ApprovalStatus
    {
        public long Id { get; set; }
        public string? status { get; set; }
        public string? opis { get; set; }
        public string? k_ins { get; set; }
        public DateTime? d_ins { get; set; }
        public string? k_upd { get; set; }
        public DateTime? d_upd { get; set; }
        public bool? Active { get; set; }
    }
}
