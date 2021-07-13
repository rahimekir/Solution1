using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        public AdminController(IProductService productService)
        {
            this._productService = productService;
        }
        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Products = _productService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();

        }
        [HttpPost]
        public IActionResult CreateProduct(Product model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Url = model.Url,
                Price = model.Price,
                Description = model.Description,
                ImageUrl=model.ImageUrl

            };
            _productService.Add(entity);
            return RedirectToAction("ProductList");

        }
        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetById((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId= entity.ProductId,
                Name=entity.Name,
                Url = entity.Url,
                Price= entity.Price,
                Description = entity.Description,
                ImageUrl= entity.ImageUrl
            };
            return View(model);

        }
        [HttpPost]
        public IActionResult EditProduct(Product model)
        {
            var entity = _productService.GetById(model.ProductId);
            if (entity == null)
            {
                return NotFound();

            }
            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Url = model.Url;
            entity.ImageUrl = model.ImageUrl;
            entity.Description = model.Description;
            _productService.Update(entity);


            return RedirectToAction("ProductList");

        }

        
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);
            if (entity != null)
            {
                _productService.Delete(entity);

            }
            return RedirectToAction("ProductList");
            ;
        }
    }
}
