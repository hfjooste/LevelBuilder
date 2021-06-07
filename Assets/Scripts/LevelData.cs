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