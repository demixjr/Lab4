using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DAL
{
    public class Announcement
    {
        public Announcement() { }
        public Announcement( Category category, Subcategory subcategory, List<Tag> tags, string title,string description, User user)
        {
            Category = category;
            Subcategory = subcategory;
            Tags = tags;
            Title = title;
            Description = description;
            User = user;
        }

        public string GetInfo()
        {
            string tags = "";
            foreach (Tag tag in Tags)
            {
                tags += tag.Name.ToString() + ", ";
            }
            tags = tags.TrimEnd(',', ' ');

            return $"{Title}: \n {Description} Категорія: {Category.Name}\n Підкатегорія: {Subcategory.Name} \n Теги: {tags} \n ";
        }
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int SubcategoryId { get; set; }
        public virtual Subcategory Subcategory { get; set; }

        public string Username { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
