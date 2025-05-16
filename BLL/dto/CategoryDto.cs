using System.Collections.Generic;
using DAL;

namespace BLL
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public HeadingDto Heading { get; set; }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        public List<SubcategoryDto> Subcategories { get; set; }

    }
}
