using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IRepository<T> 
    {
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
        T Get(Predicate<T> predicate);
        List<T> GetAll(Predicate<T> predicate);

    }
}
