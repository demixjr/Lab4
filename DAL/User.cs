using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(32)]
        public string Username { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
    }
}
