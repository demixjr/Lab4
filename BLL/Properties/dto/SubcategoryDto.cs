using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.dto
{
    public class SubcategoryDto
    {
        public string Name { get; set; }
        public CategoryDto Category {  get; set; }
        public List<AnnouncementDto> Announcements { get; set; }
    }
}
