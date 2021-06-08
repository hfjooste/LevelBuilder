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
    using UnityEngine;
    using UnityEngine.Assertions;

    /// <summary>
    /// Load a level that is specified in the editor
    /// </summary>
    public class StaticLevelLoader : MonoBehaviour
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
        private void Awake()
        {
            Assert.IsNotNull(level);
            LevelLoader.LoadLevel(level);
        }
        #endregion
    }
}