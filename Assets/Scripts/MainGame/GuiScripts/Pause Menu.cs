using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string scenename;
    public string currentscene;
    void Start()
    {
        currentscene = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
    }
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(scenename);
    }
    public void ResetScene()
    {
        SceneManager.LoadSceneAsync(currentscene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
