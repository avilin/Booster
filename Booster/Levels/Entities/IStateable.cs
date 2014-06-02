using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Booster.Levels.Entities
{
    public interface IStateable
    {
        HashSet<EntityStates> CurrentEntityStates { get; set; }
    }
}
