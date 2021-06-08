namespace ThirdPixelGames.LevelBuilder
{
    using System;

    using UnityEngine;

    /// <summary>
    /// A level that can be loaded if we're using the specified ID
    /// </summary>
    [Serializable]
    public struct LevelIndexItem
    {
        /// <summary>
        /// The ID linked to this level (used to load the level)
        /// </summary>
        [Tooltip("The ID linked to this level (used to load the level)")]
        public int id;

        /// <summary>
        /// The level to load
        /// </summary>
        [Tooltip("The level to load")]
        public Level level;
    }
}