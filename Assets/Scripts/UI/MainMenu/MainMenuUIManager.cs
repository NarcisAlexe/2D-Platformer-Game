using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject settingsOptions;

    [SerializeField] private GameObject sfxSlider;
    [SerializeField] private GameObject musicSlider;



    private void Awake()
    {
        mainMenuScreen.SetActive(true);
        options.SetActive(true);
        settingsOptions.SetActive(false);
        sfxSlider.SetActive(false);
        musicSlider.SetActive(false);
    }


    public void Play()
    {
        SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadScene(1);
    }



    public void Settings()
    {
        options.SetActive(false);
        settingsOptions.SetActive(true);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false; // Exit play mode in Editor
        #else
                Application.Quit(); // Quit application in build
        #endif
    }

    public void ShowSFXSlider()
    {
        musicSlider.SetActive(false);
        sfxSlider.SetActive(true);
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSFXViaSlider();
    }

    public void ShowMusicSlider()
    {
        sfxSlider.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(true);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicViaSlider();
    }

    public void Back()
    {
        options.SetActive(true);
        settingsOptions.SetActive(false);
        sfxSlider.gameObject.SetActive(false);
        musicSlider.gameObject.SetActive(false);
    }
}
