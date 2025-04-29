using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DAL
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        public ICollection<Announcement> Announcements { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
    }
}