using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("quit");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}