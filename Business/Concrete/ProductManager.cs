using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product entity)
        {
            _productDal.Add(entity);
        }

        public void Delete(Product entity)
        {

            _productDal.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll();

        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productDal.GetCountByCategory(category);
        }

        public List<Product> GetHomePageProducts()
        {
            return _productDal.GetHomePageProducts();
        }

        public Product GetProductDetails(string url)
        {
           return _productDal.GetProductDetails(url);
        }
        public List<Product> GetProductsByCategory(string name, int page, int pageSize = 0)
        {
            return _productDal.GetProductsByCategory(name,page,pageSize);
        }

        public List<Product> GetSearchResult(string searchString)
        {
            return _productDal.GetSearchResult(searchString);
        }

        public void Update(Product entity)
        {
             _productDal.Update(entity);
        }
    }
}
