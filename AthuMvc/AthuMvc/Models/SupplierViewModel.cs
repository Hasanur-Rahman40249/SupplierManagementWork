using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthuMvc.Models
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }
        [Required]
        [StringLength(50)]
        public string SupplierName { get; set; }
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        public int GoodsId { get; set; }

        [Required]
        [StringLength(50)]
        public string GoodsName { get; set; }

    }
}