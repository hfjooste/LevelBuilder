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
    using UnityEngine.Assertions;

    public class DynamicLevelLoader : MonoBehaviour
    {
        #region Unity Methods
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            // Get the level index component
            var levelIndex = GetComponent<LevelIndex>();
            Assert.IsNotNull(levelIndex);

            // Get the correct level based on the level ID
            var level = levelIndex.levels.FirstOrDefault(fd => fd.id == GetLevelId());
            LevelLoader.LoadLevel(level.level);
        }
        #endregion

        #region Public Virtual Methods
        /// <summary>
        /// Get the level ID that we're supposed to load
        /// </summary>
        /// <returns>The integer ID linked to the level</returns>
        public virtual int GetLevelId()
        {
            return PlayerPrefs.GetInt("Level", 1);
        }
        #endregion
    }
}