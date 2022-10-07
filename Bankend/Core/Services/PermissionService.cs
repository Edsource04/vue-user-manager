using Core.Context;
using Core.Entities;
using Core.Interface;
using Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly UserManagerContext userManager;
        public PermissionService(UserManagerContext context)
        {
               userManager = context;   
        }

        public PermissionTypeViewModel CreatePermissionType(CreatePermissionTypeViewModel viewModel)
        {
            var permissionType = new PermissionType();
            permissionType.Description = viewModel.Description;
            userManager.PermissionTypes?.Add(permissionType);
            userManager.SaveChanges();

            return new PermissionTypeViewModel
            {
                Id = permissionType.Id,
                Description = permissionType.Description
            };
        }

        public PermissionViewModel CreatePermission(CreatePermissionViewModel viewModel)
        {
            Permission permission = new Permission();
            permission.EmployeeName = viewModel.EmployeeName;
            permission.EmployeeLastName = viewModel.EmployeeLastName;

            var permissionType = userManager.PermissionTypes?.FirstOrDefault(c => c.Id == viewModel.PermissionTypeId);
            
            if (permissionType != null)
                permission.PermissionType = permissionType;
                else
                permissionType = new PermissionType();

            userManager.Permissions?.Add(permission);
            userManager.SaveChanges();

            if(permission.PermissionType is not null)
            return new PermissionViewModel(permission.Id)
            {
                EmployeeName = permission.EmployeeName,
                EmployeeLastName = permission.EmployeeLastName,
                PermissionType = new PermissionTypeViewModel { Id = permission.PermissionType.Id, Description = permission.PermissionType.Description},
                Date = permission.Date
            };
            else return 
             new PermissionViewModel(permission.Id)
            {
                EmployeeName = permission.EmployeeName,
                EmployeeLastName = permission.EmployeeLastName,
                PermissionType = new PermissionTypeViewModel { Id = 0, Description = ""},
                Date = permission.Date
            };
        }

        public bool UpdatePermissionType(UpdatePermissionTypeViewModel viewModel)
        {
            PermissionType? permissionType = userManager.PermissionTypes?.Find(viewModel.Id);

            if (permissionType == null)
                return false;


             permissionType.Description = viewModel.Description;
             userManager.Entry<PermissionType>(permissionType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                userManager.SaveChanges();
                
           return true;
        }

        public bool UpdatePermission(UpdatePermissionViewModel viewModel)
        {
            Permission? permission = userManager.Permissions?.Find(viewModel.Id);

            if (permission is null)
                return false;


                permission.EmployeeName = viewModel.EmployeeName; 
                permission.EmployeeLastName= viewModel.EmployeeLastName;
                permission.Date = viewModel.Date;
                var permissionType = userManager.PermissionTypes?.FirstOrDefault(c => c.Id == viewModel.PermissionTypeId);
                if (permissionType is not null)
                {
                    permission.PermissionType = permissionType;
                }

                userManager.Entry<Permission>(permission).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                userManager.SaveChanges();

                return true;


        }

        public bool DeletePermission(int Id)
        {
            if (Id == 0)
                return false;

            if (userManager.Permissions is not null)
            {
                Permission? permission = userManager.Permissions.Find(Id);

                if (permission is null)
                    return false;
                else
                {
                    userManager.Permissions.Remove(permission);
                    userManager.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool DeletePermissionType(int Id)
        {
            if (Id == 0)
                return false;

            if(userManager.PermissionTypes is not null)
            {
                PermissionType? permissionType = userManager.PermissionTypes.Find(Id);
                if (permissionType is null)
                    return false;
                else
                {
                    userManager.PermissionTypes.Remove(permissionType);
                    userManager.SaveChanges();
                    return true;
                }
                   
            }

            return false;
        }

        public List<PermissionViewModel> GetPermissions()
        {
            List<PermissionViewModel> permissionViewModels = new List<PermissionViewModel>();

            if (userManager.Permissions is not null)
            {
                var permissions = userManager.Permissions.Include(pt => pt.PermissionType).IgnoreAutoIncludes().ToList();
                
                foreach(Permission p in permissions)
                {
                    PermissionViewModel permissions1 = new PermissionViewModel(p.Id);
                    if (p.PermissionType is not null)
                        permissions1.PermissionType = new PermissionTypeViewModel { Id = p.PermissionType.Id, Description = p.PermissionType.Description };
                    permissions1.EmployeeLastName = p.EmployeeLastName;
                    permissions1.EmployeeName = p.EmployeeName;
                    permissions1.Date = p.Date;
                    permissionViewModels.Add(permissions1);
                }
            }
         
                return permissionViewModels;
        }

        public List<PermissionTypeViewModel> GetPermissionTypes()
        {
            var permissionTypesVM = new List<PermissionTypeViewModel>();
            if (userManager.PermissionTypes is not null)
            {
                var permissionTypes = userManager.PermissionTypes.ToList();
                foreach(PermissionType pt in permissionTypes)
                {
                    permissionTypesVM.Add(new PermissionTypeViewModel { Id = pt.Id, Description = pt.Description});
                }          
            }

            return permissionTypesVM;
        }

        public PermissionTypeViewModel GetPermissionTypeById(int Id)
        {
            if (Id == 0) return new PermissionTypeViewModel();

            if (userManager.PermissionTypes is not null)
            {
                var permissionType = userManager.PermissionTypes.Find(Id);
                if (permissionType != null)
                    return new PermissionTypeViewModel { Id = permissionType.Id, Description = permissionType.Description};
                else
                    return new PermissionTypeViewModel();
            }
            else
                return new PermissionTypeViewModel();
        }

        public PermissionViewModel GetPermissionById(int Id)
        {
            if (Id == 0) return new PermissionViewModel();

            if (userManager.Permissions is not null)
            {
                var permission = userManager.Permissions.Include("PermissionType").FirstOrDefault(x => x.Id == Id);
                if (permission != null && permission.PermissionType != null)
                {
                    PermissionViewModel per = new PermissionViewModel(permission.Id)
                    {
                        EmployeeLastName = permission.EmployeeLastName,
                        Date = permission.Date,
                        EmployeeName = permission.EmployeeName,
                        PermissionType = new PermissionTypeViewModel { Id = permission.PermissionType.Id, Description = permission.PermissionType.Description},
                    };
                    return per;
                }
                return new PermissionViewModel();
            }
            return new PermissionViewModel();
        }
    }
}
