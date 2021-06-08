namespace ThirdPixelGames.LevelBuilder
{
    using System.Collections.Generic;

    using UnityEngine;

    /// <summary>
    /// The palette used to generate levels
    /// </summary>
    [CreateAssetMenu(fileName = "Palette", menuName = "Level Builder/Palette", order = 1)]
    public class Palette : ScriptableObject
    {
        /// <summary>
        /// A list of all items in this palette
        /// </summary>
        [Tooltip("A list of all items in this palette")]
        public List<PaletteItem> items;
    }
}