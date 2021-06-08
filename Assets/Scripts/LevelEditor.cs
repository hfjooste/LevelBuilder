/*
 2021 © Third Pixel Games. All Rights Reserved

 All information contained herein is and remains the property of Third Pixel Games. The intellectual 
 and technical concepts contained herein are proprietary to Third Pixel Games and may be covered by 
 patents and patents in process and are protected by trade secret and copyright laws. Dissemination 
 of this information or reproduction of this material (including source code) is strictly forbidden 
 unless prior written consent is obtained from Third Pixel Games.
*/

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
        /// The palette selected index
        /// </summary>
        private int _selected = 0;
        #endregion

        #region Unity Methods
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            // Find the properties we need to build a level
            _sizeX = serializedObject.FindProperty("sizeX");
            _sizeY = serializedObject.FindProperty("sizeY");
            _savedPalette = serializedObject.FindProperty("palette");
            _levelData = serializedObject.FindProperty("data");
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

            // Edit the Size X and Size Y variables
            EditorGUILayout.PropertyField(_sizeX);
            EditorGUILayout.PropertyField(_sizeY);

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
            EditorGUILayout.Space(10);

            // Add a clear level button
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Clear level"))
            {
                // Reset the level data if the button is pressed
                _levelData.stringValue = string.Empty;
            }
            EditorGUILayout.EndHorizontal();

            // Ensure we have a valid Size X and Size Y value
            var sizeX = (int)Mathf.Max(_sizeX.intValue, 0);
            var sizeY = (int)Mathf.Max(_sizeY.intValue, 0);

            // Try to load the saved level data
            var data = string.IsNullOrEmpty(_levelData.stringValue) || _levelData.stringValue.Replace(" ", "") == "{}" 
                ? new List<LevelData>() : JsonHelper.FromJson<LevelData>(_levelData.stringValue).ToList();

            // Populate the level data variable (if needed)
            for (var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    // Check if we have a saved item at this position
                    var item = data.FirstOrDefault(fd => fd.x == x && fd.y == y);
                    if (item != null)
                    {
                        // Skip this item to avoid losing data
                        continue;
                    }
                    
                    // Add a new (empty) item at this position
                    data.Add(new LevelData()
                    {
                        x = x,
                        y = y,
                        paletteId = string.Empty
                    });
                }
            }

            // Loop through the level data
            for (var y = 0; y < sizeY; y++)
            {
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
                    var button = GUILayout.Button(string.Empty, GUILayout.Width(40), GUILayout.Height(40));
                    if (button)
                    {
                        // If the button is pressed, update the palette ID
                        item.paletteId = _selected < 0 || _selected >= palette.items.Count 
                            ? string.Empty : palette.items[_selected].id;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }

            // Save the level data
            _levelData.stringValue = JsonHelper.ToJson(data.ToArray());

            // Apply all changes to the serialized object
            serializedObject.ApplyModifiedProperties();
        }
        #endregion
    }
}
#endif