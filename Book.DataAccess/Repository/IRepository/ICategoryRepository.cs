using BookMVCWebsite.Models;

namespace Book.DataAccess.Repository.IRepository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);
    void Save();
}
