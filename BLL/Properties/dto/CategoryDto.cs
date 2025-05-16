using System.Collections.Generic;
using DAL;

namespace BLL.dto
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public HeadingDto Heading { get; set; }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        public List<SubcategoryDto> Subcategories { get; set; }
        public List<AnnouncementDto> Announcements { get; set; }

    }
}
