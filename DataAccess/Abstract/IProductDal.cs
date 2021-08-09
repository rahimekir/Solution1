
using Core.DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Abstract
{
 public interface IProductDal:IEntityRepository<Product>
    {
        Product GetProductDetails(string url );
        Product GetByIdWithCategories(int id);
        List<Product> GetSearchResult(string searchString);
        List<Product> GetHomePageProducts();
        List<Product> GetProductsByCategory(string name,int page,int pageSize );
        int GetCountByCategory(string category);
        void Update(Product entity, int[] categoryIds);


    }
}
