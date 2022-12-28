using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Refuntations_App_Data.Model
{
    public class OnlineUser: IdentityUser
    {
        [Required(ErrorMessage ="Ovo polje je obavezno.")]
        [EmailAddress(ErrorMessage ="Nevalidna email adresa.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        public string? Password{ get; set; }
        public bool Active { get; set; } = true;
        public int K_Ins { get; set; }
        public string? D_Ins { get; set; } = DateTime.Now.ToString();
        public int K_Upd { get; set; }
        public string? D_Upd { get; set; } = DateTime.Now.ToString();
    }
}
