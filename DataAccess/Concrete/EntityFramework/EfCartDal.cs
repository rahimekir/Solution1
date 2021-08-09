using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCartDal : EfCoreRepository<Cart, EtContext>, ICartDal
    {
        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new EtContext())
            {
                var cmd = @"delete from CartItems where CartId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmd, cartId, productId);
            }
        }

        public Cart GetByUserId(string userId)
        {
            using(var context = new EtContext())
            {
                return context.Carts
                            .Include(i => i.CartItems)
                            .ThenInclude(i => i.Product)
                            .FirstOrDefault(i => i.UserId == userId);


            }
        }
        public override void Update(Cart entity)
        {
            using (var context = new EtContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
