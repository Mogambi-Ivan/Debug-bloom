using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleted : MonoBehaviour
{
    /// <summary>
    /// Called when the "Return to Main Menu" button is clicked.
    /// </summary>
    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Main Menu…");
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Optional: Called when the "Quit Game" button is clicked.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting Game…");
        Application.Quit();
    }
}
