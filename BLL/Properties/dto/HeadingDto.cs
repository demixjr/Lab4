﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.dto
{
    public class HeadingDto
    {
        public string Name { get; set; }
        public ICollection<CategoryDto> Categories  { get; set; }
    }
}
