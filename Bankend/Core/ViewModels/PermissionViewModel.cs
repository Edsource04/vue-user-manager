using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class PermissionViewModel
    {
        public PermissionViewModel(int id)
        {
            Id = id;
        }
        public PermissionViewModel()
        {

        }

        public int Id { get; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public PermissionTypeViewModel? PermissionType { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
