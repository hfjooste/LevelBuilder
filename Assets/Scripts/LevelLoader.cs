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
    using System.Linq;

    using UnityEngine;

    /// <summary>
    /// The object responsible for reading the level file and generating the level
    /// </summary>
    public class LevelLoader : MonoBehaviour
    {
        #region Public Variables
        /// <summary>
        /// The level data to load
        /// </summary>
        [Tooltip("The level data to load")]
        public Level level;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        public void Awake()
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
                var instance = Instantiate(paletteItem.prefab);

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
