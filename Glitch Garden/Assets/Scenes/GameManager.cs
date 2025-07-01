using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private int maxLevel = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        int nextLevel = currentLevel + 1;

        if (nextLevel > maxLevel)
        {
            nextLevel = maxLevel;
        }

        currentLevel = nextLevel;
        SceneManager.LoadScene(nextLevel);

    }
}
