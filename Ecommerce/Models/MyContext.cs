using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models;

public  class MyContext : DbContext
{

    public MyContext(DbContextOptions<MyContext> options)
        : base(options)
    {
    }

    public  DbSet<TblAdmin> tbl_admin { get; set; }

    public  DbSet<TblCart> tbl_cart { get; set; }

    public  DbSet<TblCategory> tbl_category { get; set; }

    public  DbSet<TblCustomer> tbl_customer { get; set; }

    public  DbSet<TblFaq> tbl_faq { get; set; }

    public  DbSet<TblFeedback> tbl_feedback { get; set; }

    public  DbSet<TblProduct> tbl_product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblProduct>() //entity method hy jo model/table ko represent kar raha
        .HasOne(p=>p.Category)
        .WithMany(c=>c.Product)
        .HasForeignKey(p=>p.Cat_id);

        modelBuilder.Entity<TblCart>()
        .HasOne(c => c.Product)
        .WithMany()
        .HasForeignKey(c => c.Prod_id);
    }

}
