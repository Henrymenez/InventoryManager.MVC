using InventoryManager.BLL.Implementation;
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

        [HttpPost]
        public async Task<IActionResult> Add(SalesViewModel model)

        {
            if (ModelState.IsValid)
            {
                var (successful, msg, obj) = await _salesServices.Add(model);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index", routeValues: new { Id = obj.UserId } );
                }
                ViewBag.ErrMsg = msg;
                return View("AddNew");
            }

            return View("AddNew");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!(id > 0))
            {
                return NotFound();
            }
            var saleFound = await _salesServices.GetSalesById(id);
            if (saleFound == null)
            {
                return NotFound();
            }

            return View(saleFound);
        }

        [HttpPost]
        public async Task<IActionResult> Save(SalesViewModel sale)
        {

            if (ModelState.IsValid)
            {

                var (successful, msg) = await _salesServices.EditSaleAsync(sale);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction(controllerName: "Product", actionName: "Index");
                }
                ViewBag.ErrMsg = msg;
                return View("Edit");
            }
            return View("Edit");
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _salesServices.GetSalesById(id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int userId, int Id)
        {
            var (successful, msg) = await _salesServices.DeleteSaleAsync(userId, Id);

            if (successful)
            {
                TempData["SuccessMsg"] = msg;
                return RedirectToAction(controllerName: "Product", actionName: "Index");
            }

            @TempData["ErrMsg"] = msg;
            return View("Details");
        }


    }
}
