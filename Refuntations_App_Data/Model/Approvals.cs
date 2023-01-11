using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Refuntations_App_Data.Model
{
    public class Approvals
    {
        [Key]
        public long Id { get; set; }
        public int fk_edokument { get; set; }
        public int? sifra_dobavljaca { get; set; }
        public string? sifra_dobavljaca_SAP { get; set; }
        public long? PIB { get; set; }
        public string? naziv_dobavljaca { get; set; }
        public string? broj_dokumenta { get; set; }
        public DateTime? datum_dokumenta { get; set; }
        public DateTime? datum_knjizenja { get; set; }
        public string? status_odobrenja { get; set; }
        public string? SEF_status { get; set; }
        public DateTime? zadnja_promena_SEF { get; set; }
        public decimal? Iznos_poreza_10 { get; set; }
        public decimal? PDV_10 { get; set; }
        public decimal? Iznos_poreza_20 { get; set; }
        public decimal? PDV_20 { get; set; }
        public decimal? iznos_neto { get; set; }
        public decimal? iznos_bruto { get; set; }
        public DateTime? Datum_prijema { get; set; }
    }
}
