using baitap.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap.Models
{
    public class CartModel
    {
        // GET: CartModel
        public Products Products { get; set; }
        public int Quantity{get;set;}
    }
}