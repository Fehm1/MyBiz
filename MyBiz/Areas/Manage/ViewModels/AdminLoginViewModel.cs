using System.ComponentModel.DataAnnotations;

namespace MyBiz.Areas.Manage.ViewModels
{
    public class AdminLoginViewModel
    {
        [StringLength(150)]
        public string UserName { get; set; }

        [StringLength(maximumLength:20,MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
