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