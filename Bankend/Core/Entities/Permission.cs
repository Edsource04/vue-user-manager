using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public PermissionType PermissionType { get; set; } = new PermissionType();
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
