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
    public class EfProductDal : EfCoreRepository<Product, EtContext>, IProductDal
    {
        public Product GetByIdWithCategories(int id)
        {
            using (var context = new EtContext())
            {
                return context.Products
                    .Where(i => i.ProductId == id)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                        .FirstOrDefault();

            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new EtContext())
            {
                var products = context.Products.Where(i => i.IsApproved).AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products

                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category)
                        .Where(i => i.ProductCategories
                        .Any(a => a.Category.Url == category));
                }
                return products.Count();
            }

        }

        public List<Product> GetHomePageProducts()
        {
            using (var context = new EtContext())
            {
                return context.Products.Where(i => i.IsApproved && i.IsHome == true).ToList();
            }
        }

        public List<Product> GetPopularProducts()
        {
            using (var context = new EtContext())
            {
                return context.Products.ToList();
            }
        }

        public Product GetProductDetails(string url)
        {
            using (var context = new EtContext())
            {
                return context.Products
                                .Where(i => i.Url == url)
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.Category)
                                .FirstOrDefault();

            }
        }


        public List<Product> GetProductsByCategory(string name, int page, int pageSize)
        {
            using (var context = new EtContext())
            {
                var products = context.Products.Where(i => i.IsApproved).AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    products = products

                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Category)
                        .Where(i => i.ProductCategories
                        .Any(a => a.Category.Url == name));
                }
                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public List<Product> GetSearchResult(string searchString)
        {
            using (var context = new EtContext())
            {
                var products = context.Products.Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchString) || i.Description.ToLower().Contains(searchString))).AsQueryable();

                return products.ToList();
            }
        }

        public void Update(Product entity, int[] categoryIds)
        {

            using (var context = new EtContext())
            {
                var product = context.Products.Include(i => i.ProductCategories)
                    .FirstOrDefault(i => i.ProductId == entity.ProductId);
                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Price = entity.Price;
                    product.Description = entity.Description;
                    product.Url = entity.Url;
                    product.ImageUrl = entity.ImageUrl;
                    product.IsHome = entity.IsHome;
                    product.IsApproved = entity.IsApproved;

                    product.ProductCategories = categoryIds.Select(cateid => new ProductCategory()
                    {
                        ProductId=entity.ProductId,
                        CategoryId=cateid,

                    }).ToList();
                    context.SaveChanges();
                }
            }

        }
    }
}
