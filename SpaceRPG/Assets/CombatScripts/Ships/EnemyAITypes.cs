using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.CombatScripts.Ships
{
    /// <summary>
    /// A collection of possible enemy AI types
    /// </summary>
    public enum EnemyAITypes
    {
        /// <summary>
        /// The enemy will charge straight at the player
        /// </summary>
        Aggressive,

        /// <summary>
        /// The enemy will try to maintain distance with the player
        /// </summary>
        Zoning,

        /// <summary>
        /// The enemy will try to run as far away from the player as possible
        /// </summary>
        Escaping
    }
}
