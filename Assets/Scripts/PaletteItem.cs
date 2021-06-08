namespace ThirdPixelGames.LevelBuilder
{
    using System;

    using UnityEngine;

    /// <summary>
    /// The data used to generate a specific tile
    /// </summary>
    [Serializable]
    public class PaletteItem
    {
        /// <summary>
        /// The name of the item displayed when designing levels
        /// </summary>
        [Tooltip("The name of the item displayed when designing levels")]
        public string name;

        /// <summary>
        /// The ID used to identify which object to spawn
        /// </summary>
        [Tooltip("The ID used to identify which object to spawn")]
        public string id = Guid.NewGuid().ToString();

        /// <summary>
        /// The color displayed in the level builder
        /// </summary>
        [Tooltip("The color displayed in the level builder")]
        public Color color;

        /// <summary>
        /// The prefab to instantiate
        /// </summary>
        [Tooltip("The prefab to instantiate")]
        public GameObject prefab;

        /// <summary>
        /// The offset applied to the prefab instance
        /// </summary>
        [Tooltip("The offset applied to the prefab instance")]
        public Vector3 offset;
    }
}
