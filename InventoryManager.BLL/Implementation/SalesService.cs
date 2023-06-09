﻿using InventoryManager.BLL.Interfaces;
using InventoryManager.BLL.Models;
using InventoryManager.DAL.Entities;
using TodoList.DAL.Repository;

namespace InventoryManager.BLL.Implementation
{
    public class SalesService : ISalesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Sales> _salesRepo;
        private readonly IRepository<Product> _productRepo;


        public SalesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _salesRepo = _unitOfWork.GetRepository<Sales>();
            _productRepo = _unitOfWork.GetRepository<Product>();
        }

        public async Task<(bool successful, string msg, SalesViewModel obj)> Add(SalesViewModel model)
        {
            var product = await _productRepo.GetByIdAsync(model.ProductId);
            if (product == null)
            {
                return (false, "Product Id Incorrect", null);
            }

            if (product.UserId != model.UserId)
            {
                return (false, "UserId Incorrect", null);
            }
            if (!(product.Quantity > model.Quantity))
            {
                return (false, "Insufficient Product Quantity", null);
            }
            product.Quantity = product.Quantity - model.Quantity;

            var newSale = new Sales
            {
                ProductId = product.Id,
                UserId = model.UserId,
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                Category = model.Category
            };

            _salesRepo.Add(newSale);
            _productRepo.Update(product);

            var rowChanged = await _unitOfWork.SaveChangesAsync();
            return rowChanged > 0 ? (true, "Added sale", model) : (false, "Unable to add sale", null);
        }

        public async Task<SalesViewModel> GetSale(int SaleId)
        {
            var sale = await _productRepo.GetByIdAsync(SaleId);
            return new SalesViewModel
            {

                UserId = sale.UserId,
                Name = sale.Name,
                Category = sale.Category,
                Quantity = sale.Quantity,
                Price = sale.Price,
                ProductId = sale.Id
            };
        }

        public IEnumerable<SalesViewModel> GetUserSales(int UserId)
        {
            var products =  _salesRepo.GetQueryable(p => p.UserId == UserId);
            return products.Select(s => new SalesViewModel
            {
                Id = s.Id,
                ProductId = s.ProductId,
                UserId = s.UserId,
                Name = s.Name,
                Category = s.Category,
                Price = s.Price,
                Quantity = s.Quantity
            });

        }

        public async Task<SalesViewModel> GetSalesById(int id)
        {
            Sales sale = await _salesRepo.GetByIdAsync(id);
            return new SalesViewModel
            {
                Id = sale.Id,
                UserId = sale.UserId,
                Name = sale.Name,
                ProductId = sale.ProductId,
                Category = sale.Category,
                Quantity = sale.Quantity,
                Price = sale.Price
            };
        }

        public async Task<(bool successful, string msg)> EditSaleAsync(SalesViewModel model)
        {
            Sales sale = await _salesRepo.GetByIdAsync(model.Id);
            if (sale == null)
            {
                return (false, $"Sale with id:{model.Id} wasn't found");
            }

            sale.Name = model.Name;
            sale.Price = model.Price;
            sale.Quantity = model.Quantity;
            sale.Category = model.Category;

            return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"Sale with Id:{model.Id} was Updated") : (false, $"Update Failed");


        }

        public async Task<(bool successful, string msg)> DeleteSaleAsync(int userId, int saleId)
        {

            Sales sale = await _salesRepo.GetByIdAsync(saleId);

            if (sale == null)
            {
                return (false, $"Sale with id:{saleId} wasn't found");
            }

          
            if (sale.UserId == userId)
            {
                _salesRepo.DeleteById(saleId);

                return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"Sale with Title:{sale.Name} was deleted") : (false, $"Delete Failed");
            }
            return (false, $"User with id:{userId} was not found");

        }

    }
}
