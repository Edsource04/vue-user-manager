using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class PermissionTypeViewModel
    {
        public PermissionTypeViewModel(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public PermissionTypeViewModel()
        {

        }

        public int Id { get; }
        public string Description { get; set; } = string.Empty;
    }
}
