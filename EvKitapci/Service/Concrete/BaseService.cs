using EvKitapci.Contexts;
using EvKitapci.Entities.Abstractions;
using EvKitapci.Entities.Enums;
using EvKitapci.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Service.Concrete
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        public BaseService(AppDbContext context)
        {
            this._context = context;
        }
        public void Add(T entity)
        {
            if (entity is not null)
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Ekleme işleminde hata oldu.");
            }
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public void Delete(T entity)
        {
            if (entity is not null)
            {
                entity.DeletedDate = DateTime.Now;
                entity.Status = Status.Passive;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("silme işleminde hata oldu.");
            }
        }

        public IList<T> GetAll()
        {
            return _context.Set<T>().Where(e => e.Status != Status.Passive).ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(e => e.Status != Status.Passive && e.Id == id);
        }

        public T GetDefault(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(e => e.Status != Status.Passive).FirstOrDefault(expression);
        }

        public IList<T> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(e => e.Status != Status.Passive).Where(expression).ToList();
        }

        public void Update(T entity)
        {
            if (entity is not null)
            {
                entity.UpdateDate = DateTime.Now;
                entity.Status = Status.Modified;
                _context.Set<T>().Update(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Güncelleme işleminde hata oldu.");
            }
        }
    }
}
