using System.ComponentModel.DataAnnotations;

namespace MyBiz.Models
{
    public class Position
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public List<Team>? Teams { get; set; }
    }
}
