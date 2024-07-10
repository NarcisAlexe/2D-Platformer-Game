using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Loading")]
    [SerializeField] public Animator anim;
    public float transitionTime = 1f;

    [Header("Sound Parameters")]
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private GameObject sfxSlider;
    [SerializeField] private GameObject musicSlider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        sfxSlider.SetActive(false);
        musicSlider.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //If pause screen already active unpause and viceversa
            if (pauseScreen.activeInHierarchy)
            {
                SoundManager.instance.PlaySound(pauseSound);
                PauseGame(false);
            }
            else
            {
                SoundManager.instance.PlaySound(pauseSound);
                PauseGame(true);
            }
        }
    }

    #region Game Over
    //Activate game over screen
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Exit play mode in Editor
        #else
                Application.Quit(); // Quit application in build
        #endif
    }
    #endregion


    #region Pause
    public void PauseGame(bool status)
    {
        //If status == true then pause | if status == false then unpause
        pauseScreen.SetActive(status);

        //When pause status is true change timescale to 0 | when it's false change it back to 1
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void ShowSFXSlider()
    {
        musicSlider.SetActive(false);
        sfxSlider.SetActive(true);
    }

    public void SoundVolume()
    {
        //SoundManager.instance.ChangeSoundVolume(0.2f);
        SoundManager.instance.ChangeSFXViaSlider();
    }

    public void ShowMusicSlider()
    {
        sfxSlider.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(true);
    }

    public void MusicVolume()
    {
        //SoundManager.instance.ChangeMusicVolume(0.2f);
        SoundManager.instance.ChangeMusicViaSlider();
    }
    #endregion

    #region You Won

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
}
