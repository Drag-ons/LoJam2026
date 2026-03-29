using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateController : MonoBehaviour
{
    public GameObject player;
    public GameObject playerGui;
    public GameObject overlayGui;
    public GameObject deathmenu;
    public GameObject enemySpawner;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameTimer;
    public string mainMenuScene;

    private string currentScene;
    private float timeElapsed;
    private PlayerMovement playerMovement;
    private PlayerResourceController playerResource;
    private EnemySpawner enemySpawnerScript;

    void Start()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.SoundType.MusicTheme);
        currentScene = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
        playerMovement = player.GetComponent<PlayerMovement>();
        playerResource = player.GetComponent<PlayerResourceController>();
        enemySpawnerScript = enemySpawner.GetComponent<EnemySpawner>();
    }

    void Update()
    {
        if (playerResource.isDead)
        {
            playerGui.SetActive(false);
            overlayGui.SetActive(false);
            playerMovement.canDash = false;
            playerMovement.canMove = false;
            playerMovement.xVelocity = 0;
            playerMovement.yVelocity = 0;
            playerResource.canBeDamaged = false;
            playerResource.canGainAbility = false;
            enemySpawnerScript.canSpawn = false;
            deathmenu.SetActive(true);
            TimeSpan finalTime = TimeSpan.FromSeconds(timeElapsed);
            scoreText.text = "You Survived For " + string.Format("{0:00}:{1:00}", finalTime.Minutes, finalTime.Seconds);
        }
        else
        {
            this.timeElapsed += Time.deltaTime;
            TimeSpan timeElapsed = TimeSpan.FromSeconds(this.timeElapsed);
            gameTimer.text = "Timer : " + string.Format("{0:00}:{1:00}", timeElapsed.Minutes, timeElapsed.Seconds);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(mainMenuScene);
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(currentScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
