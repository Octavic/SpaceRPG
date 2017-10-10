using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GeneralScripts.Quest
{
    public class ReputationQuestAvailability : QuestAvailability
    {
        public override bool IsAvailable()
        {
            return true;
        }
    }
}