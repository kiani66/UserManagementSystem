﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } // مانند "ViewUsers", "EditUsers"
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
