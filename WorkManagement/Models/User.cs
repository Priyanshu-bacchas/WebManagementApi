using System;
using System.Collections.Generic;

namespace WorkManagement.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<WorkItem> WorkItemAssignedToNavigations { get; set; } = new List<WorkItem>();

    public virtual ICollection<WorkItem> WorkItemCreatedByNavigations { get; set; } = new List<WorkItem>();
}
