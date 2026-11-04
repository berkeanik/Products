using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class ProductService : DbService<Product>, IService<ProductRequest, ProductResponse>
    {
        public ProductService(DbContext db) : base(db)
        {
        }

        // Product <-> Category
        // Product <-> ProductStores <-> Store
        protected override IQueryable<Product> DbQuery()
        {
            return base.DbQuery()
                .Include(p => p.Category)
                .Include(p => p.ProductStores)
                .ThenInclude(ps => ps.Store)
                .OrderBy(p => p.Category.Title)
                .ThenBy(p => p.Name);
        }

        public IQueryable<ProductResponse> Query()
        {
            return DbQuery().Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                UnitPrice = p.UnitPrice,
                StockAmount = p.StockAmount,
                ExpirationDate = p.ExpirationDate,
                CategoryId = p.CategoryId,
                StoreIds = p.ProductStores.Select(ps => ps.StoreId).ToList(),
                UnitPriceR = p.UnitPrice.ToString("C2"),
                StockAmountR = p.StockAmount.HasValue ? p.StockAmount.Value.ToString() : string.Empty,
                ExpirationDateR = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToShortDateString() : string.Empty,
                CategoryTitleR = p.Category.Title,
                StoreNamesR = string.Join("<br>", p.ProductStores.OrderBy(ps => ps.Store.Name).Select(ps => ps.Store.Name))
            });
        }

        public ProductRequest Edit(int id)
        {
            var productEntity = DbSingle(id);
            if (productEntity is null)
                return null;
            return new ProductRequest
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                UnitPrice = productEntity.UnitPrice,
                StockAmount = productEntity.StockAmount,
                ExpirationDate = productEntity.ExpirationDate,
                CategoryId = productEntity.CategoryId,
                StoreIds = productEntity.StoreIds
            };
        }

        public CommandResponse Add(ProductRequest request)
        {
            if (DbQuery().Any(p => p.Name == request.Name.Trim()))
                return Error("Product with the same name exists!");
            var productEntity = new Product
            {
                Name = request.Name.Trim(),
                UnitPrice = request.UnitPrice ?? 0,
                StockAmount = request.StockAmount,
                ExpirationDate = request.ExpirationDate,
                CategoryId = request.CategoryId ?? 0,
                StoreIds = request.StoreIds ?? new List<int>()
            };
            DbAdd(productEntity);
            return Success(productEntity.Id, "Product created successfully.");
        }

        public CommandResponse Update(ProductRequest request)
        {
            if (DbQuery().Any(p => p.Id != request.Id && p.Name == request.Name.Trim()))
                return Error("Product with the same name exists!");
            var productEntity = DbSingle(request.Id);
            if (productEntity is null)
                return Error("Product not found!");
            // delete the old ProductStore records, the new ones are added with StoreIds below
            DbRemove(productEntity.ProductStores);
            productEntity.Name = request.Name.Trim();
            if (request.UnitPrice.HasValue)
                productEntity.UnitPrice = request.UnitPrice.Value;
            productEntity.StockAmount = request.StockAmount;
            productEntity.ExpirationDate = request.ExpirationDate;
            if (request.CategoryId.HasValue)
                productEntity.CategoryId = request.CategoryId.Value;
            productEntity.StoreIds = request.StoreIds ?? new List<int>();
            DbUpdate(productEntity);
            return Success(productEntity.Id, "Product updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var productEntity = DbSingle(id);
            if (productEntity is null)
                return Error("Product not found!");
            // delete the relational ProductStore records first
            DbRemove(productEntity.ProductStores);
            DbRemove(productEntity);
            return Success(productEntity.Id, "Product deleted successfully.");
        }
    }
}
