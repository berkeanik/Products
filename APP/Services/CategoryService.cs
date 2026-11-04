using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class CategoryService : DbService<Category>, IService<CategoryRequest, CategoryResponse>
    {
        public CategoryService(DbContext db) : base(db)
        {
        }

        // products are included to be used in Query and Remove methods
        protected override IQueryable<Category> DbQuery()
        {
            return base.DbQuery()
                .Include(c => c.Products)
                .OrderBy(c => c.Title);
        }

        public IQueryable<CategoryResponse> Query()
        {
            return DbQuery().Select(c => new CategoryResponse
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ProductCountR = c.Products.Count,
                ProductNamesR = string.Join(", ", c.Products.OrderBy(p => p.Name).Select(p => p.Name))
            });
        }

        public CategoryRequest Edit(int id)
        {
            var categoryEntity = DbSingle(id);
            if (categoryEntity is null)
                return null;
            return new CategoryRequest
            {
                Id = categoryEntity.Id,
                Title = categoryEntity.Title,
                Description = categoryEntity.Description
            };
        }

        public CommandResponse Add(CategoryRequest request)
        {
            if (DbQuery().Any(c => c.Title == request.Title.Trim()))
                return Error("Category with the same title exists!");
            var categoryEntity = new Category
            {
                Title = request.Title.Trim(),
                Description = request.Description?.Trim()
            };
            DbAdd(categoryEntity);
            return Success(categoryEntity.Id, "Category created successfully.");
        }

        public CommandResponse Update(CategoryRequest request)
        {
            if (DbQuery().Any(c => c.Id != request.Id && c.Title == request.Title.Trim()))
                return Error("Category with the same title exists!");
            var categoryEntity = DbSingle(request.Id);
            if (categoryEntity is null)
                return Error("Category not found!");
            categoryEntity.Title = request.Title.Trim();
            categoryEntity.Description = request.Description?.Trim();
            DbUpdate(categoryEntity);
            return Success(categoryEntity.Id, "Category updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var categoryEntity = DbSingle(id);
            if (categoryEntity is null)
                return Error("Category not found!");
            // categories with products can not be deleted
            if (categoryEntity.Products.Any())
                return Error("Category has relational products!");
            DbRemove(categoryEntity);
            return Success(categoryEntity.Id, "Category deleted successfully.");
        }
    }
}
