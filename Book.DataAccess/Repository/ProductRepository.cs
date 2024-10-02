using Book.DataAccess.Repository.IRepository;
using Book.Models;
using BookMVCWebsite.DataAccess.Data;

namespace Book.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        this._db = db;
    }
    public void Update(Product product)
    {
        _db.Products.Update(product);
    }
}
