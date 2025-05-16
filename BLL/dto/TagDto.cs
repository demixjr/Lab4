using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TagDto
    {
        public string Name { get; set; }
        public List<AnnouncementDto> Announcements { get; set; }
    }
}
