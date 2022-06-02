using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCoreAuditing.Entities.Common;

namespace EntityFrameworkCoreAuditing.Entities;

public class Supplier : IAuditable
{
    [Column("SupplierId")]
    public int Id { get; set; }

    public string CompanyName { get; set; }

    public string ContactName { get; set; }

    public string City { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}
