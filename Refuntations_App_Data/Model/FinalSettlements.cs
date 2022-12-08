using Microsoft.EntityFrameworkCore;

namespace Refuntations_App_Data.Model
{
    [Keyless]
    public class FinalSettlements
    {
        public int? id_iznos_stopa_1 { get; set; }
        public int? id_iznos_stopa_2 { get; set; }
        public int? sifra_dob { get; set; }
        public string? Dobavljac { get; set; }
        public string? sifra_kat { get; set; }
        public string? Kategorija { get; set; }
        public int? Sifra_AA { get; set; }
        public string? Vrsta_AA { get; set; }
        public string? Period { get; set; }
        public decimal? Iznos_refundacije_stopa_1 { get; set; }
        public decimal? Iznos_refundacije_stopa_2 { get; set; }
        public decimal? Iznos_realizovano_stopa_1 { get; set; }
        public decimal? Iznos_realizovano_stopa_2 { get; set; }
        public DateTime? Datum_realizovano { get; set; }
        public short? status_stavke_obracuna { get; set; }
        public bool? I { get; set; }
        public bool NP { get; set; }
        public DateTime? datum_od_aa { get; set; }
        public DateTime? datum_do_aa { get; set; }
        public string? obrada { get; set; }
    }
}
