using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.dto
{
    public class AnnouncementDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }

        public CategoryDto Category { get; set; }
        public SubcategoryDto Subcategory { get; set; }
        public ICollection<TagDto> Tags { get; set; }

        public string GetInfo()
        {
            string tags = "";
            foreach (var tag in Tags)
            {
                tags += tag.Name.ToString() + ", ";
            }
            tags = tags.TrimEnd(',', ' ');

            return $"Автор {Username}: {Title}  \n {Description}  Категорія: {Category.Name}\n Підкатегорія: {Subcategory.Name} \n Теги: {tags} \n ";
        }
    }
}
