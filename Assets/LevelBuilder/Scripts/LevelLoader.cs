namespace ThirdPixelGames.LevelBuilder
{
    using System.Collections.Generic;
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

            // Load the additional layer data
            var additionalLayers = new List<LevelData[]>();
            foreach (var layer in level.additionalLayers)
            {
                additionalLayers.Add(JsonHelper.FromJson<LevelData>(layer));
            }

            // Determine the vertical offset
            var yOffset = data.Max(m => m.y) * level.scale;

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
                    var position = new Vector3(dataItem.x * level.scale, dataItem.y * level.scale, 0.0f);
                    var offset = level.levelType == LevelType.TwoDimensional ? dataPaletteItem.offset2D : dataPaletteItem.offset3D;
                    offset += new Vector3(0.0f, yOffset, 0.0f);
                    InstantiateItem(dataPaletteItem.prefab, position, offset, level.scale, level.levelType);
                }

                // Check if we've found a valid palette item for the overlay object
                if (overlayPaletteItem != null)
                {
                    // Instantiate the item
                    var position = new Vector3(overlayItem.x * level.scale, overlayItem.y * level.scale, 0.0f);
                    var offset = level.levelType == LevelType.TwoDimensional ? overlayPaletteItem.offset2D : overlayPaletteItem.offset3D;
                    offset += new Vector3(0.0f, yOffset, 0.0f);
                    InstantiateItem(overlayPaletteItem.prefab, position, offset, level.scale, level.levelType);
                }

                // Loop through all the additional layers
                foreach (var layer in additionalLayers)
                {
                    // Check for a valid layer item
                    if (layer == null || layer.Length < i)
                    {
                        continue;
                    }

                    // Get the item we're supposed to instantiate
                    var layerItem = layer[i];

                    // Find the palette item to use
                    var layerPaletteItem = level.palette.items.FirstOrDefault(fd => fd.id == layerItem.paletteId);

                    // Check if we've found a valid palette item for the layer object
                    if (layerPaletteItem != null)
                    {
                        // Instantiate the item
                        var position = new Vector3(layerItem.x * level.scale, layerItem.y * level.scale, 0.0f);
                        var offset = level.levelType == LevelType.TwoDimensional ? layerPaletteItem.offset2D : layerPaletteItem.offset3D;
                        offset += new Vector3(0.0f, yOffset, 0.0f);
                        InstantiateItem(layerPaletteItem.prefab, position, offset, level.scale, level.levelType);
                    }
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
        /// <param name="scale">The scale applied to the new instance</param>
        /// <param name="levelType">The type of level to generate</param>
        private static void InstantiateItem(GameObject prefab, Vector3 position, Vector3 offset, float scale, LevelType levelType)
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
            
            // Set the scale of the new instance
            var scaleX = instance.transform.localScale.x * scale;
            var scaleY = instance.transform.localScale.y * scale;
            var scaleZ = instance.transform.localScale.z * scale;
            instance.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
        #endregion
    }
}
