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
        public string name;

        /// <summary>
        /// The ID used to identify which object to spawn
        /// </summary>
        public string id = Guid.NewGuid().ToString();

        /// <summary>
        /// The color displayed in the level builder
        /// </summary>
        public Color color;

        /// <summary>
        /// The prefab to instantiate
        /// </summary>
        public GameObject prefab;
    }
}
