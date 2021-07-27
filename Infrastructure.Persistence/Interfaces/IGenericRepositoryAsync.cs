using System;
using System.Collections.Generic; 
using System.Threading.Tasks; 

namespace Infrastructure.Persistence
{
    public interface IGenericRepositoryAsync<T> where T : class
    { 
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(string Include);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize, string Include); 
        Task<T> AddAsync(T entity); 
    }
}
