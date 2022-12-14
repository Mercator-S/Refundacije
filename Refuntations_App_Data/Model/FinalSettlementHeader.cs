using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Refuntations_App_Data.Model
{
    [Table("tab_refundacije_konacni_obracun_zaglavlje")]
    [Keyless]
    public class FinalSettlementHeader
    {
        public int Id { get; set; }
        public int Godina { get; set; }
        public int Mesec { get; set; }
        public short Status { get; set; }
        public string k_ins { get; set; }
        public DateTime d_ins { get; set; }
        public string k_uns { get; set; }
        public DateTime d_upd { get; set; }
        public bool Active { get; set; }
    }
}


