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