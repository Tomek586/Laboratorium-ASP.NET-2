using Data;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorium_3___App.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrganizationsApiController(AppDbContext context)
        {
            _context = context;
        }

        [Route("filter")]
        public IActionResult GetFilteredOrganizations(string q)
        {
            var result = _context.Organizations
                .Where(o => o.Name.ToUpper().StartsWith(q.ToUpper()))
                .Select(o => new
                {
                    Id = o.Id,
                    Name = o.Name,
                })
                .ToList();
            return Ok(result);
        }

        [Route("{id}")]
        public IActionResult GetOrganizationById(int id)
        {
            var entity = _context.Organizations.Find(id);
            if (entity == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(entity);
            }
        }
    }
}
