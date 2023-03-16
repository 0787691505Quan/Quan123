using baitap.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace baitap.Models
{
    public class HomeModel
    {
        public List<Products> ListProducts { get; set; }
        public List<Category> ListCategory { get; set; }
    }
}