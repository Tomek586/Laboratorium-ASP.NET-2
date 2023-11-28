using Laboratorium_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laboratorium_2.Controllers
{

    public enum Operators
    {
        ADD, SUB, MUL, DIV
    }
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Result([FromForm] Calculator model)
        {
            if (!model.IsValid())
            {
                return View("Error");
            }
            return View(model);
        }
    }
}