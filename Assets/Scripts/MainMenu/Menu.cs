using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject controlsWindow;
    public GameObject controlsNext;
    public GameObject controlsBack;
    public GameObject controlsText1;
    public GameObject controlsText2;

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void OpenControls()
    {
        controlsWindow.SetActive(true);
    }

    public void NextControls()
    {
        controlsNext.SetActive(false);
        controlsBack.SetActive(true);
        controlsText1.SetActive(false);
        controlsText2.SetActive(true);
    }

    public void BackControls()
    {
        controlsNext.SetActive(true);
        controlsBack.SetActive(false);
        controlsText1.SetActive(true);
        controlsText2.SetActive(false);
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
