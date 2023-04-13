using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace baitap.Models
{

    [MetadataType(typeof(UserMasterData))]
    public partial class User
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }

    }
    
}