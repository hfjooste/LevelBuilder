namespace ThirdPixelGames.LevelBuilder
{
    using UnityEngine;

    /// <summary>
    /// Contains all the data you need to generate a level
    /// </summary>
    [CreateAssetMenu(fileName = "Level", menuName = "Level Builder/Level", order = 0)]
    public class Level : ScriptableObject
    {
        /// <summary>
        /// The level's name
        /// </summary>
        public string levelName;
        
        /// <summary>
        /// The type of level to generate
        /// </summary>
        public LevelType levelType;

        /// <summary>
        /// The palette used to generate this level
        /// </summary>
        public Palette palette;
        
        /// <summary>
        /// The scale applied to each item in the level
        /// </summary>
        public float scale = 1.0f;

        /// <summary>
        /// The horizontal size of the level
        /// </summary>
        public int sizeX;

        /// <summary>
        /// The vertical size of the level
        /// </summary>
        public int sizeY;

        /// <summary>
        /// The serialized level data
        /// </summary>
        public string data;

        /// <summary>
        /// The serialized level overlay data
        /// </summary>
        public string overlay;

        /// <summary>
        /// The amount of additional layers
        /// </summary>
        public int additionalLayersCount;

        /// <summary>
        /// The serialized additional layer data
        /// </summary>
        public string[] additionalLayers;
    }
}