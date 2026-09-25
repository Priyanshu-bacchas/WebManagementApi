using System;
using System.Collections.Generic;

namespace WorkManagement.Models;

public partial class WorkItem
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? AssignedTo { get; set; }

    public int CreatedBy { get; set; }

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateOnly? DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? AssignedToNavigation { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User? CreatedByNavigation { get; set; } = null!;

    public virtual Project? Project { get; set; } = null!;
}
