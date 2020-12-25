using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthuMvc.Models
{
    public class tblGoods
    {
        public tblGoods()
        {
            tblSuppliers = new HashSet<tblSupplier>();
        }
        [Key]
        public int GoodsId { get; set; }
        [Required]
        [StringLength(50)]
        public string GoodsName { get; set; }

        public decimal GoodsPrice { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PDT { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        public string ImageUrl { get; set; }


        public virtual ICollection<tblSupplier> tblSuppliers { get; set; }
        
    }
}