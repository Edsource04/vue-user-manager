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

        private readonly ILogger<PermissionType> _logger;
        private readonly IPermissionService service;

        public Permissions(ILogger<PermissionType> logger, IPermissionService serviceContext)
        {
            _logger = logger;
            service = serviceContext;
        }

        [HttpGet(Name = "Permissions")]
        public ActionResult<IEnumerable<PermissionTypeViewModel>> Get()
        {
            return Ok(service.GetPermissions());
        }

        [HttpGet("{id}")]
        public PermissionViewModel Get(int id)
        {
            return service.GetPermissionById(id);
        }

        [HttpPost()]
        public  ActionResult<PermissionTypeViewModel> Post([FromBody] CreatePermissionViewModel permissionTypeForCreation)
        {
            var permissionType = service.CreatePermission(permissionTypeForCreation);

            return CreatedAtAction(nameof(Get), new { id = permissionType.Id }, permissionType);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, PermissionViewModel permission)
        {
            var perType = service.GetPermissionById(id);
            if (perType == null) return NotFound();

            UpdatePermissionViewModel toUpdate = new UpdatePermissionViewModel
            {
                Id = id,
                ViewModel = new CreatePermissionViewModel { 
                    EmployeeName = permission.EmployeeName,
                    EmployeeLastName = permission.EmployeeLastName,
                    Date = permission.Date,
                    PermissionType = permission.PermissionType
                }
            };
            if (service.UpdatePermission(toUpdate))
            {
                return NoContent();
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