using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class MyDbcontext:DbContext
    {

    public MyDbcontext(DbContextOptions<MyDbcontext> options) : base(options)
    {


    }
    public DbSet<Users> users { get; set; }

    public DbSet<ProductType> productTypes { get; set; }

    public DbSet<Products> products { get; set; }

    public DbSet<Contact>contacts { get; set; }

    public  DbSet<Shopping_Order>shopping_Orders { get; set; }


}

