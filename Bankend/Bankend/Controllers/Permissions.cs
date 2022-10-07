using Core.Interface;
using Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Bankend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Permissions : ControllerBase
    {

        private readonly ILogger<Permissions> _logger;
        private readonly IPermissionService service;

        public Permissions(ILogger<Permissions> logger, IPermissionService serviceContext)
        {
            _logger = logger;
            service = serviceContext;
        }

        [HttpGet(Name = "Permissions")]
        public ActionResult<IEnumerable<PermissionViewModel>> Get()
        {
            return Ok(service.GetPermissions());
        }

        [HttpGet("{id}")]
        public PermissionViewModel Get(int id)
        {
            return service.GetPermissionById(id);
        }

        [HttpPost()]
        public  PermissionViewModel Post([FromBody] CreatePermissionViewModel permissionForCreation)
        {
            var permissionVM = service.CreatePermission(permissionForCreation);

            return permissionVM;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, PermissionViewModel permission)
        {
            var perType = service.GetPermissionById(id);
            if (perType == null) return NotFound();

            if (permission.PermissionType is not null)
            {
                UpdatePermissionViewModel toUpdate = new UpdatePermissionViewModel
                {
                    Id = id,
                    EmployeeName = permission.EmployeeName,
                    EmployeeLastName = permission.EmployeeLastName,
                    PermissionTypeId = permission.PermissionType.Id,
                    Date = permission.Date

                };

                if (service.UpdatePermission(toUpdate))
                {
                    return NoContent();
                }
                else
                    return BadRequest();
            }
            else
                return BadRequest();
            
           
           
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var permission = service.GetPermissionById(id);

            if (permission == null)
                return NotFound();

            if (service.DeletePermission(id))
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}