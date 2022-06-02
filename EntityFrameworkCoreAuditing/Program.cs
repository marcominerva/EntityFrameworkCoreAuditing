using EntityFrameworkCoreAuditing;
using EntityFrameworkCoreAuditing.Entities;
using Microsoft.EntityFrameworkCore;

using var dataContext = new DataContext();

var supplier = new Supplier { ContactName = "Donald Duck", CompanyName = "Frullino Inc.", City = "Paperopoli" };
dataContext.Suppliers.Add(supplier);
await dataContext.SaveChangesAsync();

supplier = await dataContext.Suppliers.FirstAsync();
supplier.City = "Taggia";
await dataContext.SaveChangesAsync();

supplier = dataContext.Suppliers.Include(s => s.Products).First();
supplier.ContactName = "Mickey Mouse";
supplier.Products.Add(new Product
{
    Category = new Category
    {
        Name = "Category",
        Description = "Category Description"
    },
    Name = "Frullino",
    UnitsInStock = 1000,
    UnitPrice = 42,
    QuantityPerUnit = "I"
});

await dataContext.SaveChangesAsync();

Console.ReadLine();
