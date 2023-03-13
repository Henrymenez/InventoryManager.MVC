using InventoryManager.BLL.Interfaces;
using InventoryManager.BLL.Models;
using InventoryManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using TodoList.DAL.Repository;

namespace InventoryManager.BLL.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<User> _userRepo;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepo = _unitOfWork.GetRepository<Product>();
            _userRepo = _unitOfWork.GetRepository<User>();
        }

        public IEnumerable<ProductViewModel> GetUserProduct(int id)
        {
            var products = _productRepo.GetQueryable(p => p.UserId == id);
            return products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Quantity = p.Quantity,
                Category = p.Category,
                BrandName = p.BrandName,
                UserId = p.UserId
            }); ;

        }

        public async Task<(bool successful, string msg)> AddProductAsync(ProductViewModel product)
        {
            try
            {
                User user = await _userRepo.GetSingleByAsync(u => u.Id == product.UserId, tracking: true);

                if (user == null)
                {
                    return (false, $"User with id:{product.UserId} wasn't found");
                }

                var newProd = new Product
                {
                    UserId = product.UserId,
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    BrandName = product.BrandName
                };
                _productRepo.Add(newProd);

                var rowChanges = await _unitOfWork.SaveChangesAsync();

                return rowChanges > 0 ? (true, $"Product: {product.Name} was successfully created!") : (false, "Failed To save changes!");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        public async Task<(bool successful, string msg)> DeleteProductAsync(int userId, int prodId)
        {

            User user = await _userRepo.GetSingleByAsync(u => u.Id == userId,
                include: u => u.Include(x => x.Products.Where(u => u.UserId == userId)), tracking: true);

            if (user == null)
            {
                return (false, $"User with id:{userId} wasn't found");
            }

            var userProduct = user.Products.FirstOrDefault(p => p.Id == prodId);
            if (userProduct != null)
            {
                user.Products.Remove(userProduct);

                return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"Product with Title:{userProduct.Name} was deleted") : (false, $"Delete Failed");
            }
            return (false, $"Product with id:{prodId} was not found");

        }

        public async Task<(bool successful, string msg)> EditProductAsync(ProductViewModel product)
        {
            User user = await _userRepo.GetSingleByAsync(u => u.Id == product.UserId, include: u => u.Include(x => x.Products.Where(u => u.UserId == product.UserId)), tracking: true);
            if (user == null)
            {
                return (false, $"User with id:{product.UserId} wasn't found");
            }
            Product userProduct = user.Products.FirstOrDefault(t => t.Id == product.Id);
            if (userProduct != null)
            {
                userProduct.Name = product.Name;
                userProduct.Description = product.Description;
                userProduct.Price = product.Price;
                userProduct.Quantity = product.Quantity;
                userProduct.BrandName = product.BrandName;
                userProduct.Category = product.Category;

                return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"Product with Id:{product.Id} was Updated") : (false, $"Update Failed");
            }
            return (false, $"Product with id:{product.Id} was not found");
        }

        public async Task<ProductViewModel> GetProductById(int id)
        {
            Product product = await _productRepo.GetByIdAsync(id);
            return new ProductViewModel
            {
                Id = product.Id,
                UserId = product.UserId,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Quantity = product.Quantity,
                Price = product.Price,
                BrandName = product.BrandName
            };
        }


    }
}
