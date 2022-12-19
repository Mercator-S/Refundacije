using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Refuntations_App_Data.Model
{
    [Table("tab_refundacije_sif_dobavljaci_mail_import")]
    public class EmailImport
    {
        [Key]
        public string sap_sifra { get; set; }
        public string mail { get; set; }
    }
}
