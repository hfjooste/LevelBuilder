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
        public Level level;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        public void Awake()
        {
            var data = JsonHelper.FromJson<LevelData>(level.data);
            var xOffset = data.Max(m => m.x);
            var yOffset = data.Max(m => m.y);

            foreach (var item in data)
            {
                var paletteItem = level.palette.items.FirstOrDefault(fd => fd.id == item.paletteId);
                if (paletteItem == null)
                {
                    continue;
                }

                var instance = Instantiate(paletteItem.prefab);
                var x = item.x + paletteItem.offset.x;
                var y = -item.y + yOffset + paletteItem.offset.y;
                var z = paletteItem.offset.z;
                instance.transform.position = new Vector3(x, y, z);
            }
        }
        #endregion
    }
}
