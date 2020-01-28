using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaMedica.Utils
{
  public interface IRepository<T>
  {
    void Add(T entity);
    Task AddAsync(T entity);
    void Delete(T entity);
    Task DeleteAsync(T entity);
    void Update(T entity);
    Task UpdateAsync(T entity);
  }
}
