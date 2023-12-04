using Laboratorium_3___App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Laboratorium_3___App.Controllers
{
    public class ContactController : Controller
    {
        static List<Contact> _contact = new List<Contact>();
        static int index = 1;

        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            var x = _contactService.FindAll();
            return View(x);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Contact model = new Contact();
            model.Organizations = _contactService
                .FindAllOrganizations()
                .Select(o => new SelectListItem() { Value = o.Id.ToString(), Text = o.Name })
                .ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Contact model)
        {
            if (ModelState.IsValid)
            {
                _contactService.Add(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var contact = _contactService.FindById(id);

            contact.Organizations = _contactService
                .FindAllOrganizations()
                .Select(oe => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = oe.Name,
                    Value = oe.Id.ToString(),

                }
                ).ToList();

            if (contact != null)
            {
                return View(contact);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(Contact model)
        {
            if (ModelState.IsValid)
            {
                _contactService.Update(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {

            var contact = _contactService.FindById(id);

            contact.Organizations = _contactService
                .FindAllOrganizations()
                .Select(oe => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = oe.Name,
                    Value = oe.Id.ToString(),

                }
                ).ToList();

            if (contact != null)
            {
                return View(contact);
            }
            return View(contact);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = _contactService.FindById(id);
            if (contact == null)
            {
                return NotFound();
            }

            _contactService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}