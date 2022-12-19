using Microsoft.EntityFrameworkCore;

namespace Refuntations_App_Data.Model
{
    [Keyless]
    public class Partner
    {
        public int? sifra_dob { get; set; }
        public string? sap_sifra_dob { get; set; }
        public string? naziv_partnera { get; set; }
    }
}
