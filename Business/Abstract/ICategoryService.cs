using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService:IValidator<Category>
    {

        Category GetByIdWithProducts(int categoryId);
        void DeleteFromCategory(int productId, int categoryId);
        Category GetById(int id);

        List<Category> GetAll();

        void Add(Category entity);

        void Update(Category entity);
        void Delete(Category entity);
    }

}
