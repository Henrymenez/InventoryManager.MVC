using InventoryManager.BLL.Interfaces;
using InventoryManager.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManager.MVC.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesServices _salesServices;
        //private readonly IProductService _productService;

        public SalesController(ISalesServices salesServices)
        {
            _salesServices = salesServices;
            // _productService = productService;
        }

        public async Task<IActionResult> Index(int Id)
        {

            var sales = await _salesServices.GetUserSales(Id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }


        public async Task<IActionResult> AddNew(int id)
        {
       var sale =  await  _salesServices.GetSale(id);
            return View(sale);
        }

        public async Task<IActionResult> Add(SalesViewModel model)

        {
            if (ModelState.IsValid)
            {
                var (successful, msg, obj) = await _salesServices.Add(model);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction(actionName: "Index",controllerName: "Sales", routeValues: new { Id = obj.UserId } );
                }
                ViewBag.ErrMsg = msg;
                return View("AddNew");
            }

            return View("AddNew");
        }
    }
}
