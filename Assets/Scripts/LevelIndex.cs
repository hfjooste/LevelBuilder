namespace ThirdPixelGames.LevelBuilder
{
    using System.Collections.Generic;

    using UnityEngine;

    /// <summary>
    /// A list of all available levels linked to an integer ID
    /// </summary>
    public class LevelIndex : MonoBehaviour
    {
        /// <summary>
        /// A list of all levels that can be loaded
        /// </summary>
        [Tooltip("A list of all levels that can be loaded")]
        public List<LevelIndexItem> levels;
    }
}