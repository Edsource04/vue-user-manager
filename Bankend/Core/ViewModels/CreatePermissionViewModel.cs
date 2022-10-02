using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class CreatePermissionViewModel
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public PermissionType PermissionType { get; set; } = new PermissionType();
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
