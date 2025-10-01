using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GamePlay()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
