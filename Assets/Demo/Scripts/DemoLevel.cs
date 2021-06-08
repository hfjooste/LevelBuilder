using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A demo class used to set the level index and generate the correct level
/// </summary>
public class DemoLevel : MonoBehaviour
{
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