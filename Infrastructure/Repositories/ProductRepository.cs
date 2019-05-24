using Domain;
using Domain.Interface;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext _context;
        public ProductRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<ProductModel> AddProduct(ProductModel product)
        {
            var addProduct = new Product().Assign(product);
            _context.Set<Product>().Add(addProduct);
            await _context.SaveChangesAsync();
            product.ProductId = addProduct.Id;
            return new ProductModel().Assign(addProduct);
        }

        public async Task<List<ProductModel>> GetAllProduct()
        {
            var allProduct = await _context.Set<Product>()
                 .Where(p => p.Quantity != 0)
                 .Select(p => new ProductModel
                 {
                     ProductId = p.Id,
                     Price = p.Price,
                     ProductName = p.ProductName,
                     PhotoUrl = p.PhotoUrl,
                     UserId = p.UserId,
                     Quantity = p.Quantity
                 }).ToListAsync();
            return allProduct;
        }

        public async Task<ProductModel> GetProduct(long id)
        {
            var query = await _context.Set<Product>().FirstOrDefaultAsync(p => p.Id == id);
            var product = new ProductModel().Assign(query);
            product.ProductId= query.Id;
            return product;
        }

        public Task<ProductModel> UpdateProduct(ProductModel model)
        {
            throw new NotImplementedException();
        }
    }
}
