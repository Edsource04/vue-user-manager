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
    public class PermissionType : ControllerBase
    {

        private readonly ILogger<PermissionType> _logger;
        private readonly IPermissionService service;

        public PermissionType(ILogger<PermissionType> logger, IPermissionService serviceContext)
        {
            _logger = logger;
            service = serviceContext;
        }

        [HttpGet(Name = "PermissionTypes")]
        public ActionResult<IEnumerable<PermissionTypeViewModel>> Get()
        {
            return Ok(service.GetPermissionTypes());
        }

        [HttpGet("{id}")]
        public PermissionTypeViewModel Get(int id)
        {
            return service.GetPermissionTypeById(id);
        }

        [HttpPost()]
        public  ActionResult<PermissionTypeViewModel> Post([FromBody] CreatePermissionTypeViewModel permissionTypeForCreation)
        {
            var permissionType = service.CreatePermissionType(permissionTypeForCreation);

            return CreatedAtAction(nameof(Get), new { id = permissionType.Id }, permissionType);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, PermissionTypeViewModel permissionType)
        {
            var perType = service.GetPermissionTypeById(id);
            if (perType == null) return NotFound();

            UpdatePermissionTypeViewModel toUpdate = new UpdatePermissionTypeViewModel
            {
                Id = id,
                createPermissionType = new CreatePermissionTypeViewModel { Description = permissionType.Description }
            };
            if (service.UpdatePermissionType(toUpdate))
            {
                return NoContent();
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var permissionType = service.GetPermissionTypeById(id);

            if (permissionType == null)
                return NotFound();

            if (service.DeletePermissionType(id))
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}