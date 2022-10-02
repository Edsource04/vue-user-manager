using Core.Context;
using Core.Entities;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IPermissionService
    {
   
         PermissionTypeViewModel CreatePermissionType(CreatePermissionTypeViewModel viewModel);

         PermissionViewModel CreatePermission(CreatePermissionViewModel viewModel);

        bool UpdatePermissionType(UpdatePermissionTypeViewModel viewModel);

        bool UpdatePermission(UpdatePermissionViewModel viewModel);

        bool DeletePermission(int Id);

        bool DeletePermissionType(int Id);

        List<PermissionViewModel> GetPermissions();

        List<PermissionTypeViewModel> GetPermissionTypes();
        PermissionTypeViewModel GetPermissionTypeById(int Id);
        PermissionViewModel GetPermissionById(int Id);
    }
}
