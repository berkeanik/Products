using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class StoreService : DbService<Store>, IService<StoreRequest, StoreResponse>
    {
        public StoreService(DbContext db) : base(db)
        {
        }

        // store products are included to be used in Query and Remove methods
        protected override IQueryable<Store> DbQuery()
        {
            return base.DbQuery()
                .Include(s => s.ProductStores)
                .ThenInclude(ps => ps.Product)
                .OrderBy(s => s.IsVirtual)
                .ThenBy(s => s.Name);
        }

        public IQueryable<StoreResponse> Query()
        {
            return DbQuery().Select(s => new StoreResponse
            {
                Id = s.Id,
                Name = s.Name,
                IsVirtual = s.IsVirtual,
                IsVirtualR = s.IsVirtual ? "Yes" : "No",
                ProductCountR = s.ProductStores.Count,
                ProductNamesR = string.Join(", ", s.ProductStores.OrderBy(ps => ps.Product.Name).Select(ps => ps.Product.Name))
            });
        }

        public StoreRequest Edit(int id)
        {
            var storeEntity = DbSingle(id);
            if (storeEntity is null)
                return null;
            return new StoreRequest
            {
                Id = storeEntity.Id,
                Name = storeEntity.Name,
                IsVirtual = storeEntity.IsVirtual
            };
        }

        public CommandResponse Add(StoreRequest request)
        {
            if (DbQuery().Any(s => s.Name == request.Name.Trim()))
                return Error("Store with the same name exists!");
            var storeEntity = new Store
            {
                Name = request.Name.Trim(),
                IsVirtual = request.IsVirtual
            };
            DbAdd(storeEntity);
            return Success(storeEntity.Id, "Store created successfully.");
        }

        public CommandResponse Update(StoreRequest request)
        {
            if (DbQuery().Any(s => s.Id != request.Id && s.Name == request.Name.Trim()))
                return Error("Store with the same name exists!");
            var storeEntity = DbSingle(request.Id);
            if (storeEntity is null)
                return Error("Store not found!");
            storeEntity.Name = request.Name.Trim();
            storeEntity.IsVirtual = request.IsVirtual;
            DbUpdate(storeEntity);
            return Success(storeEntity.Id, "Store updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var storeEntity = DbSingle(id);
            if (storeEntity is null)
                return Error("Store not found!");
            // delete the relational ProductStore records first
            DbRemove(storeEntity.ProductStores);
            DbRemove(storeEntity);
            return Success(storeEntity.Id, "Store deleted successfully.");
        }
    }
}
