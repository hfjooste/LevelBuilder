using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

using ThirdPixelGames.LevelBuilder;

/// <summary>
/// A demo class used to set the level index and generate the correct level
/// </summary>
public class DemoLevel : MonoBehaviour
{
    #region Public Variables
    /// <summary>
    /// The text object in the current scene that will display the level's name
    /// </summary>
    public Text levelName;
    #endregion
    
    #region Unity Methods
    /// <summary>
    /// Start is called on the frame when a script is enabled just before any
    /// of the Update methods are called the first time
    /// </summary>
    private void Start()
    {
        Assert.IsNotNull(levelName);
        
        var level = FindObjectOfType<StaticLevelLoader>()?.level;
        if (level == null)
        {
            level = FindObjectOfType<DynamicLevelLoader>()?.level;
        }

        if (level == null)
        {
            Debug.LogError("Unable to find level loader in scene");
            return;
        }

        levelName.text = level.levelName;
    }
    #endregion
    
    #region Public Methods
    /// <summary>
    /// Load the level with index 1
    /// </summary>
    public void LoadLevel1()
    {
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Load the level with index 2
    /// </summary>
    public void LoadLevel2()
    {
        PlayerPrefs.SetInt("Level", 2);
        PlayerPrefs.Save();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}