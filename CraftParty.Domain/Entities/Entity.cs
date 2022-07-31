using CraftParty.Domain.Enums;

namespace CraftParty.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime AddedDate { get; set; }
    
    public DateTime? UpdatedDate { get; set; }

    public EntityActivityStatus ActivityStatus { get; set; } = EntityActivityStatus.Active;
}