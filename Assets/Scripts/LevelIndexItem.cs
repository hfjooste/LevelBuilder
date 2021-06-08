/*
 2021 © Third Pixel Games. All Rights Reserved

 All information contained herein is and remains the property of Third Pixel Games. The intellectual 
 and technical concepts contained herein are proprietary to Third Pixel Games and may be covered by 
 patents and patents in process and are protected by trade secret and copyright laws. Dissemination 
 of this information or reproduction of this material (including source code) is strictly forbidden 
 unless prior written consent is obtained from Third Pixel Games.
*/

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