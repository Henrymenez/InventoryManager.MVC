using InventoryManager.BLL.Models;
using InventoryManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductViewModel> GetUserProduct(int id);
        Task<(bool successful, string msg)> AddProductAsync(ProductViewModel product);
        Task<ProductViewModel> GetProductById(int id);
       Task<(bool successful, string msg)> EditProductAsync(ProductViewModel product);

        Task<(bool successful, string msg)> DeleteProductAsync(int userId,int prodId);

    }
}
