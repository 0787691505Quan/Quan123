using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace baitap.Models
{
    public partial class ProductMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [Display(Name ="Tên sản phẩm")]
        public string Name { get; set; }
        public string Avartar { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ShortDes { get; set; }
        public string FullDescription { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> PriceDiscount { get; set; }
        public Nullable<int> TyptId { get; set; }
        public string Slug { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<bool> Delected { get; set; }
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisphayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
    }
}