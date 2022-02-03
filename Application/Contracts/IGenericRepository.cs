using System.Collections.Generic;

namespace Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T Get(int id);
        public int Update(T entity, int id);
        public int Delete(int id);
        public int Add(T entity);
    }
}
