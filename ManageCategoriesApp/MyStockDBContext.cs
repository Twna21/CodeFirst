

//using more namespaces for Enitity Framwork Core using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

public class Category
{
    public Category() { }
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
}
public class MyStock : DbContext
{
    public MyStock() { }
    // These properties map to tables in the database
    public DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStoreDB"));

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
        .Property(category => category.CategoryName)
        .IsRequired() // NOT NULL
        .HasMaxLength(40);
        //Insert data for Categories table
        modelBuilder.Entity<Category>().HasData(
        new Category { CategoryID = 1, CategoryName = "Beverages" },
        new Category { CategoryID = 2, CategoryName = "Condiments" },
        new Category { CategoryID = 3, CategoryName = "Confections" }
        );
    }
}//end MyStock class