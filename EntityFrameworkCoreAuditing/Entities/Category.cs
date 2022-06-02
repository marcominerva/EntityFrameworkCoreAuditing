using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCoreAuditing.Entities;

public class Category
{
    [Column("CategoryId")]
    public int Id { get; set; }

    [Column("CategoryName")]
    public string Name { get; set; }

    public string Description { get; set; }

    public IEnumerable<Product> Products { get; set; }
}
