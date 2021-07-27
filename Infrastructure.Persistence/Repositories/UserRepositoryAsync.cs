using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    class UserRepositoryAsync : GenericRepositoryAsync<ApplicationUser>, IUserRepositoryAsync
    {
        private readonly DbSet<ApplicationUser> _users;
        private readonly ApplicationDbContext _dbContext;
        public UserRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<ApplicationUser>();
            _dbContext = dbContext;
        }
        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        }
    }
}
