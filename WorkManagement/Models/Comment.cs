using System;
using System.Collections.Generic;

namespace WorkManagement.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int WorkItemId { get; set; }

    public int UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; } = null!;

    public virtual WorkItem? WorkItem { get; set; } = null!;
}
