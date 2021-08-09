using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfCoreRepository<Category, EtContext>, ICategoryDal
    {
        public void DeleteFromCategory(int productId, int categoryId)
        {
            using (var context = new EtContext())
            {
                var cmd = "delete from productcategory where ProductId=@p0 and CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd,productId,categoryId);
                
            }
          
        }


        public Category GetByIdWithProducts(int categoryId)
        {
            using (var context = new EtContext())
            {
                return context.Categories
                                        .Where(i => i.CategoryId == categoryId)
                                        .Include(i => i.ProductCategories)
                                        .ThenInclude(i => i.Product)
                                        .FirstOrDefault();
            }
        }
    }
}
