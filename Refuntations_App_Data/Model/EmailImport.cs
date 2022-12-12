using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
