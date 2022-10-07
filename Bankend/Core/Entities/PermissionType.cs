﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PermissionType
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;     
        public virtual List<Permission>? Permissions { get; set; }
    }
}
