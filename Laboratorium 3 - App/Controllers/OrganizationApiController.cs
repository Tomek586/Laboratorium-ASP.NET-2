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

        [HttpGet]
        public IActionResult GetFiltered(string filter)
        {
            return Ok(_context.Organizations
                .Where(o => o.Name.StartsWith(filter))
                .Select(o => new { o.Name, o.Id })
                .ToList());
        }
    }
}

