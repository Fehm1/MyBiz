using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBiz.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int PositionId { get; set; }


        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(100)]
        public string? ImageUrl { get; set; }

        [StringLength(300)]
        public string Description { get; set; }
        [StringLength(100)]
        [DataType(DataType.Url)]
        public string? InstagramUrl { get; set; }

        [StringLength(100)]
        [DataType(DataType.Url)]
        public string? FacebookUrl { get; set; }

        [StringLength(100)]
        [DataType(DataType.Url)]
        public string? TwitterUrl { get; set; }

        [StringLength(100)]
        [DataType(DataType.Url)]
        public string? LinkedInUrl { get; set; }
        public bool IsDeleted { get; set; }


        public Position? Position { get; set; }
        [NotMapped]
        public IFormFile? ImageFormFile { get; set; }
    }
}
