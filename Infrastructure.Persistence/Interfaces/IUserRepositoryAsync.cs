using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public interface IUserRepositoryAsync : IGenericRepositoryAsync<ApplicationUser>
    { 
        Task<ApplicationUser> FindByNameAsync(string userName);
    }
}
