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
            PermissionType permission = new PermissionType();
            permission.Description = viewModel.Description;
            userManager.PermissionTypes?.Add(permission);
            userManager.SaveChanges();

            return new PermissionTypeViewModel(permission.Id, permission.Description);
        }

        public PermissionViewModel CreatePermission(CreatePermissionViewModel viewModel)
        {
            Permission permission = new Permission();
            permission.EmployeeName = viewModel.EmployeeName;
            permission.EmployeeLastName = viewModel.EmployeeLastName;
            permission.PermissionType = viewModel.PermissionType;

            userManager.Permissions?.Add(permission);
            userManager.SaveChanges();

            return new PermissionViewModel(permission.Id)
            {
                EmployeeName = permission.EmployeeName,
                EmployeeLastName = permission.EmployeeLastName,
                PermissionType = permission.PermissionType,
                Date = permission.Date
            };
        }

        public bool UpdatePermissionType(UpdatePermissionTypeViewModel viewModel)
        {
            PermissionType? permissionType = userManager.PermissionTypes?.Find(viewModel.Id);

            if (permissionType == null)
                return false;


            if (viewModel.createPermissionType is not null)
            {
             permissionType.Description = viewModel.createPermissionType.Description;
             userManager.Entry<PermissionType>(permissionType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                userManager.SaveChanges();
                return true;
            }

            return false;
        }

        public bool UpdatePermission(UpdatePermissionViewModel viewModel)
        {
            Permission? permission = userManager.Permissions?.Find(viewModel.Id);

            if (permission is null)
                return false;

            if (viewModel.ViewModel is not null)
            {
                permission.EmployeeName = viewModel.ViewModel.EmployeeName; 
                permission.EmployeeLastName= viewModel.ViewModel.EmployeeLastName;
                permission.PermissionType = viewModel.ViewModel.PermissionType;
                permission.Date = viewModel.ViewModel.Date;

                userManager.Entry<Permission>(permission).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                userManager.SaveChanges();

                return true;
            }

            return false;



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
                var permissions = userManager.Permissions.Include("PermissionType").ToList();
                foreach(Permission p in permissions)
                {
                    PermissionViewModel permissions1 = new PermissionViewModel(p.Id);
                    permissions1.PermissionType = p.PermissionType;
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
                    permissionTypesVM.Add(new PermissionTypeViewModel(pt.Id, pt.Description));
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
                    return new PermissionTypeViewModel(permissionType.Id, permissionType.Description);
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
                if (permission != null)
                {
                    PermissionViewModel per = new PermissionViewModel(permission.Id)
                    {
                        EmployeeLastName = permission.EmployeeLastName,
                        Date = permission.Date,
                        EmployeeName = permission.EmployeeName,
                        PermissionType = permission.PermissionType
                    };
                    return per;
                }
                return new PermissionViewModel();
            }
            return new PermissionViewModel();
        }
    }
}
