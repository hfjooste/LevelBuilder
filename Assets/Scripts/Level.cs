/*
 2021 © Third Pixel Games. All Rights Reserved

 All information contained herein is and remains the property of Third Pixel Games. The intellectual 
 and technical concepts contained herein are proprietary to Third Pixel Games and may be covered by 
 patents and patents in process and are protected by trade secret and copyright laws. Dissemination 
 of this information or reproduction of this material (including source code) is strictly forbidden 
 unless prior written consent is obtained from Third Pixel Games.
*/

namespace LevelBuilder
{
    using UnityEngine;

    /// <summary>
    /// Contains all the data you need to generate a level
    /// </summary>
    [CreateAssetMenu(fileName = "Level", menuName = "Level Builder/Level", order = 0)]
    public class Level : ScriptableObject
    {
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