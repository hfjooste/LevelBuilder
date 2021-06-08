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
    using System.Linq;

    using UnityEngine;

    /// <summary>
    /// A static class that allows you to read data and generate the level
    /// </summary>
    public static class LevelLoader
    {
        #region Public Static Methods
        /// <summary>
        /// Load the level data and generate the level
        /// </summary>
        /// <param name="level">The level to generate</param>
        public static void LoadLevel(Level level)
        {
            // Load the selected level's data
            var data = JsonHelper.FromJson<LevelData>(level.data);

            // Determine the vertical offset
            var yOffset = data.Max(m => m.y);

            // Loop through all the items in the level's data
            foreach (var item in data)
            {
                // Find the palette item to use
                var paletteItem = level.palette.items.FirstOrDefault(fd => fd.id == item.paletteId);

                // Check if we've found a valid palette item
                if (paletteItem == null)
                {
                    // Skip this tile if we can't find a valid palette item
                    continue;
                }

                // Instantiate the prefab
                var instance = Object.Instantiate(paletteItem.prefab);

                // Set the position of the new instance
                var x = item.x + paletteItem.offset.x;
                var y = -item.y + yOffset + paletteItem.offset.y;
                var z = paletteItem.offset.z;
                instance.transform.position = new Vector3(x, y, z);
            }
        }
        #endregion
    }
}
