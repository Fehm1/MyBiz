using System.ComponentModel.DataAnnotations;

namespace MyBiz.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string? Key { get; set; }

        [StringLength(300)]
        public string? Value { get; set; }
    }
}
