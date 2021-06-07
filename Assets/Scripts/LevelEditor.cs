/*
 2021 © Third Pixel Games. All Rights Reserved

 All information contained herein is and remains the property of Third Pixel Games. The intellectual 
 and technical concepts contained herein are proprietary to Third Pixel Games and may be covered by 
 patents and patents in process and are protected by trade secret and copyright laws. Dissemination 
 of this information or reproduction of this material (including source code) is strictly forbidden 
 unless prior written consent is obtained from Third Pixel Games.
*/

#if UNITY_EDITOR
namespace LevelBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

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
            serializedObject.Update();

            _savedPalette.objectReferenceValue = (Palette)EditorGUILayout.ObjectField("Palette", _savedPalette.objectReferenceValue, typeof(Palette), false);
            var palette = _savedPalette.objectReferenceValue as Palette;
            EditorGUILayout.Space(20);
            
            if (palette == null)
            {
                EditorGUILayout.LabelField("Please specify a palette");
                return;
            }

            EditorGUILayout.PropertyField(_sizeX);
            EditorGUILayout.PropertyField(_sizeY);

            var items = new List<string>();
            foreach (var item in palette.items)
            {
                items.Add(item.name);
            }

            items.Add("Erase");

            _selected = EditorGUILayout.Popup("Item", _selected, items.ToArray());
            EditorGUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Clear level"))
            {
                _levelData.stringValue = string.Empty;
            }

            EditorGUILayout.EndHorizontal();

            var sizeX = (int)Mathf.Max(_sizeX.intValue, 0);
            var sizeY = (int)Mathf.Max(_sizeY.intValue, 0);
            var data = string.IsNullOrEmpty(_levelData.stringValue) || _levelData.stringValue.Replace(" ", "") == "{}" 
                ? new List<LevelData>() : JsonHelper.FromJson<LevelData>(_levelData.stringValue).ToList();

            for (var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    var item = data.FirstOrDefault(fd => fd.x == x && fd.y == y);
                    if (item != null)
                    {
                        continue;
                    }
                    
                    data.Add(new LevelData()
                    {
                        x = x,
                        y = y,
                        paletteId = string.Empty
                    });
                }
            }

            for (var y = 0; y < sizeY; y++)
            {
                EditorGUILayout.BeginHorizontal();

                for (var x = 0; x < sizeX; x++)
                {
                    var item = data.FirstOrDefault(fd => fd.x == x && fd.y == y);
                    var id = item.paletteId ?? string.Empty;
                    var color = string.IsNullOrEmpty(id) ? Color.white :
                        (palette.items.FirstOrDefault(fd => fd.id == id)?.color ?? Color.white);

                    GUI.backgroundColor = color;
                    var button = GUILayout.Button(string.Empty, GUILayout.Width(40), GUILayout.Height(40));
                    if (button)
                    {
                        item.paletteId = _selected < 0 || _selected >= palette.items.Count 
                            ? string.Empty : palette.items[_selected].id;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }

            _levelData.stringValue = JsonHelper.ToJson<LevelData>(data.ToArray());
            serializedObject.ApplyModifiedProperties();
        }
        #endregion
    }
}
#endif