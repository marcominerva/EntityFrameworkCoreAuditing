using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCoreAuditing.Entities;

[Table("Audits")]
public class Audit
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string EntityName { get; set; }

    [MaxLength(100)]
    public string EntityId { get; set; }

    [MaxLength(10)]
    [Required]
    public string Action { get; set; }

    public DateTime Timestamp { get; set; }

    public Dictionary<string, object> Values { get; set; }
}