using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AthuMvc.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext():base("MyCS")
        {

        }
        public DbSet<tblSupplier> tblSuppliers { get; set; }
        public DbSet<tblGoods> TblGoods { get; set; }
        public DbSet<tblUser> tblUsers { get; set; }
        public DbSet<tblRole> tblRoles { get; set; }
    }
}