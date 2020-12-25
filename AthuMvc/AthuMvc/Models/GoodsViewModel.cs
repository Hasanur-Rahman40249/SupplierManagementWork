using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthuMvc.Models
{
    public class GoodsViewModel
    {
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

        public HttpPostedFileBase ImageFile { get; set; }
    }
}