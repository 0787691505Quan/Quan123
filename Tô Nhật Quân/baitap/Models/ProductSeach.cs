using baitap.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace baitap.Models
{
    public class ProductSeach
    {
        QuanlybanhangEntities objQuanlybanhangEntities = new QuanlybanhangEntities();
        public List<Products> SearchByKey(string key)
        {
            return objQuanlybanhangEntities.Products.SqlQuery("Select * From Products Where Name like '%" + key + "%'").ToList();
        }
    }
}