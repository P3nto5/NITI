﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesMvc.Models
{
    public class SalesMvcContext : DbContext
    {
        public SalesMvcContext (DbContextOptions<SalesMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<PDF> PDFs { get; set; }
    }
}
