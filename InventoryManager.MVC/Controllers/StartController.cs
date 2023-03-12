using InventoryManager.BLL.models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.MVC.Controllers
{
    public class StartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
