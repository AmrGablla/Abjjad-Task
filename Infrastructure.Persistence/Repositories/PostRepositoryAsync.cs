using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    class PostRepositoryAsync : GenericRepositoryAsync<Post>, IPostRepositoryAsync
    {
        private readonly DbSet<Post> Posts; 
        private readonly ApplicationDbContext _dbContext;
        public PostRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            Posts = dbContext.Set<Post>(); 
            _dbContext = dbContext;
        } 
 
    }
}
