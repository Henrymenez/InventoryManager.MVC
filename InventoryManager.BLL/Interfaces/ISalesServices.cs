using InventoryManager.BLL.Models;

namespace InventoryManager.BLL.Interfaces
{
    public interface ISalesServices
    {
        IEnumerable<SalesViewModel> GetUserSales(int UserId);

        Task<(bool successful, string msg, SalesViewModel obj)> Add(SalesViewModel model);

        Task<SalesViewModel> GetSale(int SaleId);

        Task<SalesViewModel> GetSalesById(int id);

        Task<(bool successful, string msg)> EditSaleAsync(SalesViewModel sale);

        Task<(bool successful, string msg)> DeleteSaleAsync(int userId, int saleId);


    }
}
