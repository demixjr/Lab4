using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
}
