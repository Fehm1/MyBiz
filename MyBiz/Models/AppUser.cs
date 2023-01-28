using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyBiz.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(150)]
        public string FullName { get; set; }
    }
}
