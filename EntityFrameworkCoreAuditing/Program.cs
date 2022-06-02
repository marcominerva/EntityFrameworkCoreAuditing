using EntityFrameworkCoreAuditing;
using Microsoft.EntityFrameworkCore;

using var dataContext = new DataContext();

var audits = await dataContext.Audits.ToListAsync();

await dataContext.SaveChangesAsync();

Console.ReadLine();
