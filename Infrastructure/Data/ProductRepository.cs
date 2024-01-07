using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        #region Ctor
        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        private readonly StoreDbContext _context;
        #endregion


        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                                 .Include(p => p.ProductBrand)
                                 .Include(p => p.productType)
                                 .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            int typeId = 1;
            var products = _context.Products.Where(p => p.ProductTypeId == typeId);
            return await _context.Products
                                 .Include(p => p.ProductBrand)
                                 .Include(p => p.productType)
                                 .ToListAsync();
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
        
    }
}
