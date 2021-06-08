namespace ThirdPixelGames.LevelBuilder
{
    using System;

    using UnityEngine;

    /// <summary>
    /// Contains the level data for a single tile
    /// </summary>
    [Serializable]
    public class LevelData
    {
        /// <summary>
        /// The x-position of the object
        /// </summary>
        [SerializeField] public int x;

        /// <summary>
        /// The y-position of the object
        /// </summary>
        [SerializeField] public int y;

        /// <summary>
        /// The ID used by the item in the palette
        /// </summary>
        [SerializeField] public string paletteId;
    }
}