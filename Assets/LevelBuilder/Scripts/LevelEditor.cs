#if UNITY_EDITOR
namespace ThirdPixelGames.LevelBuilder
{
    using System.Linq;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEditor;

    /// <summary>
    /// The editor script that simplifies the level design
    /// </summary>
    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {
        #region Private Variables
        /// <summary>
        /// The level's name
        /// </summary>
        private SerializedProperty _levelName;
        
        /// <summary>
        /// The type of level to generate
        /// </summary>
        private SerializedProperty _levelType;
        
        /// <summary>
        /// The scale value inside the level object
        /// </summary>
        private SerializedProperty _scale;

        /// <summary>
        /// The Size X value inside the level object
        /// </summary>
        private SerializedProperty _sizeX;

        /// <summary>
        /// The Size Y value inside the level object
        /// </summary>
        private SerializedProperty _sizeY;

        /// <summary>
        /// The Palette value inside the level object
        /// </summary>
        private SerializedProperty _savedPalette;

        /// <summary>
        /// The Level Data value inside the level object
        /// </summary>
        private SerializedProperty _levelData;

        /// <summary>
        /// The Overlay Data value inside the level object
        /// </summary>
        private SerializedProperty _overlay;

        /// <summary>
        /// The amount of additional layers for the level
        /// </summary>
        private SerializedProperty _additionalLayersCount;

        /// <summary>
        /// The Additional Layer Data value inside the level object
        /// </summary>
        private SerializedProperty _additionalLayers;

        /// <summary>
        /// The palette selected index
        /// </summary>
        private int _selected = 0;

        /// <summary>
        /// The size of each block in the grid
        /// </summary>
        private int _gridSize = 35;

        /// <summary>
        /// Show/hide the grid position indicators
        /// </summary>
        private bool _showGridPosition = true;

        /// <summary>
        /// Show/hide the level data grid
        /// </summary>
        private bool _showLevelData;

        /// <summary>
        /// Show/hide the overlay data grid
        /// </summary>
        private bool _showOverlayData;

        /// <summary>
        /// Show/hide the additional layer data grid
        /// </summary>
        private bool[] _showAdditionalData;
        #endregion

        #region Unity Methods
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            // Find the properties we need to build a level
            _levelName = serializedObject.FindProperty("levelName");
            _levelType = serializedObject.FindProperty("levelType");
            _scale = serializedObject.FindProperty("scale");
            _sizeX = serializedObject.FindProperty("sizeX");
            _sizeY = serializedObject.FindProperty("sizeY");
            _savedPalette = serializedObject.FindProperty("palette");
            _levelData = serializedObject.FindProperty("data");
            _overlay = serializedObject.FindProperty("overlay");
            _additionalLayersCount = serializedObject.FindProperty("additionalLayersCount");
            _additionalLayers = serializedObject.FindProperty("additionalLayers");
        }

        /// <summary>
        /// Inside this function you can add your own custom IMGUI based GUI 
        /// for the inspector of a specific object class
        /// </summary>
        public override void OnInspectorGUI()
        {
            // Update serialized object's representation.
            serializedObject.Update();

            // Edit the selected palette
            _savedPalette.objectReferenceValue = (Palette)EditorGUILayout.ObjectField("Palette", _savedPalette.objectReferenceValue, typeof(Palette), false);
            var palette = _savedPalette.objectReferenceValue as Palette;
            EditorGUILayout.Space(20);
            
            // Check if a palette is selected
            if (palette == null)
            {
                // Display a message to the user and disable the rest of the editor
                EditorGUILayout.LabelField("Please specify a palette");
                return;
            }
            
            // Edit the level name
            EditorGUILayout.PropertyField(_levelName);
            
            // Edit the level type
            _levelType.enumValueIndex = (int)(LevelType)EditorGUILayout.EnumPopup("Level Type", (LevelType)_levelType.enumValueIndex);
            
            // Edit the scale variable
            EditorGUILayout.PropertyField(_scale);

            // Edit the Size X and Size Y variables
            EditorGUILayout.PropertyField(_sizeX);
            EditorGUILayout.PropertyField(_sizeY);
            EditorGUILayout.PropertyField(_additionalLayersCount);

            // Ensure we have valid values
            _sizeX.intValue = Mathf.Max(_sizeX.intValue, 0);
            _sizeY.intValue = Mathf.Max(_sizeY.intValue, 0);
            _additionalLayersCount.intValue = Mathf.Max(_additionalLayersCount.intValue, 0);

            // Add the palette items to the dropdown list
            var items = new List<string>();
            foreach (var item in palette.items)
            {
                items.Add(item.name);
            }

            // Add an erase item to the dropdown list
            items.Add("Erase");

            // Display the dropdown list with all the available items
            _selected = EditorGUILayout.Popup("Item", _selected, items.ToArray());
            EditorGUILayout.Space(20);

            // Toggle the grid position indicators
            _showGridPosition = EditorGUILayout.Toggle("Show Grid Positions", _showGridPosition);

            // Adjust the grid display size
            _gridSize = EditorGUILayout.IntSlider("Grid Display Size", _gridSize, 35, 100);

            // Ensure we have a valid Size X and Size Y value
            var sizeX = (int)Mathf.Max(_sizeX.intValue, 0);
            var sizeY = (int)Mathf.Max(_sizeY.intValue, 0);

            // Try to load the saved level data
            var data = GetLevelData(_levelData.stringValue, sizeX, sizeY);

            // Try to load the saved overlay data
            var overlay = GetLevelData(_overlay.stringValue, sizeX, sizeY);

            // Store the default background color
            var defaultColor = GUI.backgroundColor;

            // Generate the level grid
            data = GenerateGrid(data, ref _showLevelData, "Level Data", sizeX, sizeY);

            // Generate the overlay grid
            overlay = GenerateGrid(overlay, ref _showOverlayData, "Overlay Data", sizeX, sizeY);

            // Initialize the additional data visiblity flags
            if (_showAdditionalData == null || _showAdditionalData.Length != _additionalLayersCount.intValue)
            {
                _showAdditionalData = new bool[_additionalLayersCount.intValue];
            }

            // Try to load the saved additional layer data
            var additionalLayers = new List<LevelData[]>();
            for (var i = 0; i < _additionalLayersCount.intValue; i++)
            {
                // Get the layer data
                var additionalLayer = _additionalLayers.arraySize <= i
                    ? string.Empty
                    : _additionalLayers.GetArrayElementAtIndex(i)?.stringValue;

                // Add empty data if nothing is found
                if (string.IsNullOrEmpty(additionalLayer))
                {
                    _additionalLayers.InsertArrayElementAtIndex(i);
                }
                
                // Convert the json to LevelData
                var additionalLayerData = GetLevelData(additionalLayer, sizeX, sizeY);

                // Generate the grid
                additionalLayers.Add(GenerateGrid(additionalLayerData, ref _showAdditionalData[i],
                    $"Additional Layer #{i + 1}", sizeX, sizeY));
            }

            // Reset the background color
            GUI.backgroundColor = defaultColor;

            // Add the legend title
            GUILayout.Label("Legend", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            // Loop through all items in the palette
            foreach (var item in palette.items)
            {
                // Start the horizontal layout
                EditorGUILayout.BeginHorizontal();

                // Set the background color
                GUI.backgroundColor = item.color;

                // Display a small button for the palette item
                if (GUILayout.Button(string.Empty, GUILayout.Width(18), GUILayout.Height(18)))
                {
                    // Display the palette item's information if clicked
                    var text = $"Name: {item.name}\nID: {item.id}\nColor: {item.color}\nOffset 2D: {item.offset2D}\nOffset 3D: {item.offset3D}\nPrefab: {item.prefab}";
                    EditorUtility.DisplayDialog($"{item.name} Info", text, "OK");
                }

                // Revert the background color
                GUI.backgroundColor = defaultColor;

                // Display the palette item's name
                GUILayout.Label(item.name);

                // End the horizontal layout
                EditorGUILayout.EndHorizontal();
            }

            // Reset the background color
            GUI.backgroundColor = defaultColor;

            // Save the level data
            _levelData.stringValue = JsonHelper.ToJson(data.ToArray());

            // Save the overlay data
            _overlay.stringValue = JsonHelper.ToJson(overlay.ToArray());

            // Save the additional layer data
            _additionalLayers.ClearArray();
            for (var i = 0; i < _additionalLayersCount.intValue; i++)
            {
                _additionalLayers.InsertArrayElementAtIndex(i);
                _additionalLayers.GetArrayElementAtIndex(i).stringValue = JsonHelper.ToJson(additionalLayers[i].ToArray());
            }

            // Apply all changes to the serialized object
            serializedObject.ApplyModifiedProperties();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get a list of level data (or empty level data if nothing is saved)
        /// </summary>
        /// <param name="json">The saved level data</param>
        /// <param name="sizeX">The horizontal size of the level</param>
        /// <param name="sizeY">The vertical size of the level</param>
        /// <returns>An array of empty level data</returns>
        private LevelData[] GetLevelData(string json, int sizeX, int sizeY)
        {
            var data = new List<LevelData>();
            if (!string.IsNullOrEmpty(json))
            {
                data = JsonHelper.FromJson<LevelData>(json).ToList();
            }

            if (data != null && sizeX * sizeY > 0 && data.Count == sizeX * sizeY)
            {
                return data.ToArray();
            }

            // Generate empty level data
            data.Clear();
            for (var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    data.Add(new LevelData
                    {
                        x = x,
                        y = y,
                        paletteId = string.Empty
                    });
                }
            }

            return data.ToArray();
        }

        /// <summary>
        /// Surround the level with the selected palette item
        /// </summary>
        /// <param name="data">The current level data</param>
        /// <param name="sizeX">The horizontal size of the level</param>
        /// <param name="sizeY">The vertical size of the level</param>
        private void Surround(ref LevelData[] data, int sizeX, int sizeY)
        {
            // Get the correct palette item ID
            var palette = _savedPalette.objectReferenceValue as Palette;
            var id = _selected < 0 || _selected >= palette.items.Count
                            ? string.Empty : palette.items[_selected].id;

            // Loop through the horizontal items
            for (var x = 0; x < sizeX; x++)
            {
                // Get the top and bottom items
                var itemTop = data.FirstOrDefault(fd => fd.x == x && fd.y == 0);
                var itemBottom = data.FirstOrDefault(fd => fd.x == x && fd.y == sizeY - 1);

                // Set the palette ID
                itemTop.paletteId = id;
                itemBottom.paletteId = id;
            }

            // Loop through the vertical items
            for (var y = 0; y < sizeY; y++)
            {
                // Get the left and right items
                var itemLeft = data.FirstOrDefault(fd => fd.x == 0 && fd.y == y);
                var itemRight = data.FirstOrDefault(fd => fd.x == sizeX - 1 && fd.y == y);

                // Set the palette ID
                itemLeft.paletteId = id;
                itemRight.paletteId = id;
            }            
        }

        /// <summary>
        /// Fill the empty tiles of the level with the selected palette item
        /// </summary>
        /// <param name="data">The current level data</param>
        /// <param name="sizeX">The horizontal size of the level</param>
        /// <param name="sizeY">The vertical size of the level</param>
        private void FillEmpty(ref LevelData[] data, int sizeX, int sizeY)
        {
            // Get the correct palette item ID
            var palette = _savedPalette.objectReferenceValue as Palette;
            var id = _selected < 0 || _selected >= palette.items.Count
                            ? string.Empty : palette.items[_selected].id;

            // Find the empty items
            var emptyItems = data.Where(w => string.IsNullOrEmpty(w.paletteId));

            // Loop through the empty items
            foreach (var item in emptyItems)
            {
                // Apply the new palette item
                item.paletteId = id;
            }     
        }

        /// <summary>
        /// Generate a grid using the specified data
        /// </summary>
        /// <param name="data">The current level/overlay data</param>
        /// <param name="showGrid">Used to show/hide the generated grid</param>
        /// <param name="title">The title that is displayed above the grid</param>
        /// <param name="sizeX">The horizontal size of the level</param>
        /// <param name="sizeY">The vertical size of the level</param>
        /// <returns>The updated level data</returns>
        private LevelData[] GenerateGrid(LevelData[] data, ref bool showGrid, string title, int sizeX, int sizeY)
        {
            // Display the title
            showGrid = EditorGUILayout.BeginFoldoutHeaderGroup(showGrid, title);
            
            // Check if we're displaying the grid
            if (!showGrid || sizeX <= 0 || sizeY <= 0)
            {
                // End the foldout group
                EditorGUILayout.EndFoldoutHeaderGroup();
                return data;
            }
            
            // Add a space above the grid
            EditorGUILayout.Space(5);

            // Start the horizontal layout for the buttons
            EditorGUILayout.BeginHorizontal();

            // Add a surround button
            if (GUILayout.Button("Surround"))
            {
                // Show a confirmation dialog
                if (EditorUtility.DisplayDialog($"Surround ({title})", "Are you sure you want to surround this layer with the selected item? This process can not be reverted", "Yes", "No"))
                {
                    // Surround the layer with the selected palette item
                    Surround(ref data, sizeX, sizeY);
                }
            }

            // Add a fill button
            if (GUILayout.Button("Fill Empty"))
            {
                // Show a confirmation dialog
                if (EditorUtility.DisplayDialog($"Fill Empty ({title})", "Are you sure you want to fill the empty tiles of this layer with the selected item? This process can not be reverted", "Yes", "No"))
                {
                    // Fill the empty tiles of the layer with the selected palette item
                    FillEmpty(ref data, sizeX, sizeY);
                }
            }

            // Add a clear overlay button
            if (GUILayout.Button("Clear"))
            {
                // Show a confirmation dialog
                if (EditorUtility.DisplayDialog($"Clear ({title})", "Are you sure you want to clear this layer? This process can not be reverted", "Yes", "No"))
                {
                    // Reset the layer data if the button is pressed                    
                    data = GetLevelData(string.Empty, sizeX, sizeY);
                }
            }

            // End the horizontal layout for the buttons
            EditorGUILayout.EndHorizontal();

            // Add a space below the buttons
            EditorGUILayout.Space(10);

            // Get the current background color
            var backgroundColor = GUI.backgroundColor; 

            // Get the correct palette
            var palette = _savedPalette.objectReferenceValue as Palette;

            // Loop through the level data
            for (var y = 0; y < sizeY; y++)
            {
                // Start the horizontal layout
                EditorGUILayout.BeginHorizontal();

                for (var x = 0; x < sizeX; x++)
                {
                    // Find the correct item at this position
                    var item = data.FirstOrDefault(fd => fd.x == x && fd.y == y);

                    // Get the palette ID for this item
                    var id = item.paletteId ?? string.Empty;

                    // Set the correct color for this palette item
                    var color = string.IsNullOrEmpty(id) ? Color.white :
                        (palette.items.FirstOrDefault(fd => fd.id == id)?.color ?? Color.white);
                    GUI.backgroundColor = color;

                    // Create a button
                    var buttonText = _showGridPosition ? $"{x},{y}" : string.Empty;
                    var button = GUILayout.Button(buttonText, GUILayout.Width(_gridSize), GUILayout.Height(_gridSize));
                    if (button)
                    {
                        // If the button is pressed, update the palette ID
                        item.paletteId = _selected < 0 || _selected >= palette.items.Count
                            ? string.Empty : palette.items[_selected].id;
                    }
                }

                // End the horizontal layout
                EditorGUILayout.EndHorizontal();
            }
            
            // End the foldout group
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            // Restore the background color
            GUI.backgroundColor = backgroundColor;

            // Add a space to the bottom of the grid
            EditorGUILayout.Space(30);

            // Return the updated data
            return data;
        }
        #endregion
    }
}
#endif