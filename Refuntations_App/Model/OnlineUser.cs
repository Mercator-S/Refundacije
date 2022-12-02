using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations.Schema;


namespace Refuntations_App.Model
{
    public class OnlineUser: IdentityUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string? Password{ get; set; }
        public bool Active { get; set; } = true;
        public int K_Ins { get; set; }
        public string? D_Ins { get; set; } = DateTime.Now.ToString();
        public int K_Upd { get; set; }
        public string? D_Upd { get; set; } = DateTime.Now.ToString();
    }
}
