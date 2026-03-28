using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject controlsWindow;

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void OpenControls()
    {
        controlsWindow.SetActive(true);
    }

    public void CloseControls()
    {
        controlsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
