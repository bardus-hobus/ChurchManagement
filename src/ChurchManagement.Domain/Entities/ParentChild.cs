using ChurchManagement.Domain.Enums;

namespace ChurchManagement.Domain.Entities;

public class ParentChild
{
    private ParentChild() { } // EF Core

    public ParentChild(Guid parentId, Guid childId, KinshipType type, DateOnly since)
    {
        if (parentId == childId) throw new ArgumentException("Parent and child must differ.");
        Id = Guid.NewGuid();
        ParentId = parentId;
        ChildId = childId;
        Type = type;
        Since = since;
        Active = true;
    }

    public Guid Id { get; private set; }
    public Guid ParentId { get; private set; }
    public Guid ChildId { get; private set; }
    public KinshipType Type { get; private set; }
    public DateOnly Since { get; private set; }
    public DateOnly? Until { get; private set; }
    public bool Active { get; private set; }

    public void End(DateOnly until)
    {
        Until = until;
        Active = false;
    }
}