using Book.DataAccess.Repository.IRepository;
using BookMVCWebsite.DataAccess.Data;
using Book.Models;

namespace Book.DataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _db;
    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        this._db = db;
    }
    public void Update(Category category)
    {
        _db.Categories.Update(category);
    }
}
