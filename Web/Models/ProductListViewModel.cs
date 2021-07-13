
using System;
using System.Collections.Generic;
using Entities;

namespace Web.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ITemsPerPage { get; set; }//sayfa basına kaç ürün

        public int CurrentPage { get; set; } //secilen sayfa
        public string CurrentCategory { get; set; } // linkin sonuna gelecek kategori var mı yok mu
        //10/3=3.3=>4
      public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ITemsPerPage);
        }
    }
    public class ProductListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Product> Products { get; set; }
    }
}
