using EvKitapci.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Service.Interfaces
{
    public interface IBaseService<T> where T : BaseEntity
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IList<T> GetAll();
        T GetById(int id);
        bool Any(Expression<Func<T, bool>> expression);
        T GetDefault(Expression<Func<T, bool>> expression);
        IList<T> GetDefaults(Expression<Func<T, bool>> expression);

    }
}
