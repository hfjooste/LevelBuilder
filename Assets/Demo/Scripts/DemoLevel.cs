/*
 2021 © Third Pixel Games. All Rights Reserved

 All information contained herein is and remains the property of Third Pixel Games. The intellectual 
 and technical concepts contained herein are proprietary to Third Pixel Games and may be covered by 
 patents and patents in process and are protected by trade secret and copyright laws. Dissemination 
 of this information or reproduction of this material (including source code) is strictly forbidden 
 unless prior written consent is obtained from Third Pixel Games.
*/

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
