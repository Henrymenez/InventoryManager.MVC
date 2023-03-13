using InventoryManager.BLL.Models;
using InventoryManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.BLL.Interfaces
{
    public interface ISalesServices
    {
        Task<IEnumerable<SalesViewModel>> GetUserSales(int UserId);

        Task<(bool successful, string msg, SalesViewModel obj)> Add(SalesViewModel model);

        Task<SalesViewModel> GetSale(int SaleId);


    }
}
