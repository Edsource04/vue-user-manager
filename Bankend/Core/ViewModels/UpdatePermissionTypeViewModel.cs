﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class UpdatePermissionTypeViewModel
    {
        public int Id { get; set; }
        public CreatePermissionTypeViewModel? createPermissionType { get; set; }
    }
}