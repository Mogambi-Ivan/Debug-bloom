using UnityEngine;
using UnityEngine.SceneManagement; // For scene loading

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Called when "Play" button is clicked.
    /// Loads Level1 scene.
    /// </summary>
    public void PlayGame()
    {
        Debug.Log("Play button clicked — loading Level  1");
        SceneManager.LoadScene("Level  1"); // Make sure Level1 is added to Build Settings
    }

    /// <summary>
    /// Called when "Settings" button is clicked.
    /// Currently just logs. Implement settings UI here if needed.
    /// </summary>
    public void OpenSettings()
    {
        Debug.Log("Settings button clicked — no settings implemented yet.");
        // Optional: Load settings panel or another scene here
    }

    /// <summary>
    /// Called when "Quit" button is clicked.
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit button clicked — exiting game.");
        Application.Quit();
    }
}
