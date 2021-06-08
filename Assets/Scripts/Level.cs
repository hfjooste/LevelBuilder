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
        /// The type of level to generate
        /// </summary>
        [SerializeField] public LevelType levelType;

        /// <summary>
        /// The palette used to generate this level
        /// </summary>
        public Palette palette;

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
    }
}