//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IQuestStep.cs">
//    Copyright (c) Yifei Xu .  All rights reserved.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

namespace Assets.GeneralScripts.Quest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines a quest
    /// </summary>
    public interface IQuestStep
    {
        /// <summary>
        /// Title of the quest
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Description of the quest
        /// </summary>
        string Description { get; }

        /// <summary>
        /// How much time there will be to complete the quest
        /// </summary>
        DateTime TimeLimit { get; }

        /// <summary>
        /// Checks if the quest is finished
        /// </summary>
        bool IsFinished { get; }

        /// <summary>
        /// If this step has failed
        /// </summary>
        bool IsFailed { get; }
    }
}
