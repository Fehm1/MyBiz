using System.ComponentModel.DataAnnotations;

namespace MyBiz.ViewModels
{
    public class MemberRegisterViewModel
    {
        [StringLength(150)]
        public string UserName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }
    }
}
