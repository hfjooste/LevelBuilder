﻿namespace ThirdPixelGames.LevelBuilder
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

            // Load the selected level's overlay data
            var overlay = JsonHelper.FromJson<LevelData>(level.overlay);

            // Determine the vertical offset
            var yOffset = data.Max(m => m.y);

            // Loop through all the items in the level's data
            for (var i = 0; i < data.Length; i++)
            {
                // Get the item we're supposed to instantiate
                var dataItem = data[i];
                var overlayItem = overlay[i];

                // Find the palette item to use
                var dataPaletteItem = level.palette.items.FirstOrDefault(fd => fd.id == dataItem.paletteId);
                var overlayPaletteItem = level.palette.items.FirstOrDefault(fd => fd.id == overlayItem.paletteId);

                // Check if we've found a valid palette item for the data object
                if (dataPaletteItem != null)
                {
                    // Instantiate the item
                    var position = new Vector3(dataItem.x, dataItem.y, 0.0f);
                    var offset = level.levelType == LevelType.TwoDimensional ? dataPaletteItem.offset2D : dataPaletteItem.offset3D;
                    offset += new Vector3(0.0f, yOffset, 0.0f);
                    InstantiateItem(dataPaletteItem.prefab, position, offset, level.levelType);
                }

                // Check if we've found a valid palette item for the overlay object
                if (overlayPaletteItem != null)
                {
                    // Instantiate the item
                    var position = new Vector3(overlayItem.x, overlayItem.y, 0.0f);
                    var offset = level.levelType == LevelType.TwoDimensional ? overlayPaletteItem.offset2D : overlayPaletteItem.offset3D;
                    offset += new Vector3(0.0f, yOffset, 0.0f);
                    InstantiateItem(overlayPaletteItem.prefab, position, offset, level.levelType);
                }
            }
        }
        #endregion

        #region Private Static Methods
        /// <summary>
        /// Instantiate a new prefab at the specified position
        /// </summary>
        /// <param name="prefab">The prefab to instantiate</param>
        /// <param name="position">The position of the new instance</param>
        /// <param name="offset">The offset applied to the new instance</param>
        /// <param name="levelType">The type of level to generate</param>
        private static void InstantiateItem(GameObject prefab, Vector3 position, Vector3 offset, LevelType levelType)
        {
            // Instantiate the prefab
            var instance = Object.Instantiate(prefab);

            // Determine the position of the new instance
            var x = position.x + offset.x;
            var y = -position.y + offset.y;
            var z = offset.z;

            // Check if the level is supposed to be 3D
            if (levelType == LevelType.ThreeDimensional)
            {
                // Swap the Y and Z position
                var temp = y;
                y = z;
                z = temp;
            }

            // Set the position of the new instance
            instance.transform.position = new Vector3(x, y, z);
        }
        #endregion
    }
}
