using System.Collections.Generic;

namespace Booster.Levels.Entities
{
    public interface IStateable
    {
        HashSet<EntityStates> CurrentEntityStates { get; set; }
    }
}