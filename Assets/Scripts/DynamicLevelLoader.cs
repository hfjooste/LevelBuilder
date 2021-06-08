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