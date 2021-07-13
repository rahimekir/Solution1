using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfCoreRepository<TEntity, TContext> : IEntityRepository<TEntity>
         where TEntity : class
         where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {

                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {

                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }


 

        public List<TEntity> GetAll()
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().ToList();

            }
        }

        public TEntity GetById(int id)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Find(id);

            }
        }


        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
