using InventoryManager.BLL.Interfaces;
using InventoryManager.BLL.Models;
using InventoryManager.DAL.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InventoryManager.MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        public IActionResult Index()
        {
            IEnumerable<ProductViewModel> prodModel = _productService.GetUserProduct(1004);
            return View(prodModel);
        }

        public IActionResult AddNew(int Id)
        {
               var newProd = new ProductViewModel()
            {
               
                Name = default,
            Description = default,
            Category = default,
            Quantity = default,
            Price = default,
            BrandName = default,
            UserId = Id
        };

            return View(newProd);
           
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel model)

        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _productService.AddProductAsync(model);
                if (successful)
                {
                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
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
            var productFound = await _productService.GetProductById(id);
            if (productFound == null)
            {
                return NotFound();
            }

            return View(productFound);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel product)
        {

            if (ModelState.IsValid)
            {

                var (successful, msg) = await _productService.EditProductAsync(product);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
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

            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int userId, int Id)
        {
            var (successful, msg) = await _productService.DeleteProductAsync(userId, Id);

            if (successful)
            {
                TempData["SuccessMsg"] = msg;
                return RedirectToAction("Index");
            }

            @TempData["ErrMsg"] = msg;
            return View("Details");
        }
    }
}
