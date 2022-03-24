using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace TodoMaui.Entities;

[Table("Todo")]
public class TodoEntity
{
    [Key]
    public long TodoId { get; set; }

    public string Note { get; set; }
    
    public bool IsDone { get; set; }

    [DefaultValue(typeof(DateTime), "")]        
    public DateTime Updated { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime Inserted { get; set; } = DateTime.UtcNow;
}
