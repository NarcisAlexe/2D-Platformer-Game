using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    [SerializeField]  private AudioSource sfxSource;
    [SerializeField]  private AudioSource musicSource;

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;


    private void Awake()
    {
        instance = this;
        //sfxSource = GetComponent<AudioSource>();
        //musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip _sound)
    {
        sfxSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        //Get base volume
        float baseVolume = 1f;

        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat("soundsVolume"); //Load last saved sound volume from player prefs
        currentVolume += _change;

        //Check if we reached the maximum or minimum value
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        float finalVolume = currentVolume * baseVolume;
        sfxSource.volume = finalVolume;

        //Save final value to player prefs
        PlayerPrefs.SetFloat("soundsVolume", currentVolume);
    }

    public void ChangeMusicVolume(float _change)
    {
        //Get base volume
        float baseVolume = 1f;

        //Get initial value of volume and change it
        float currentVolume = PlayerPrefs.GetFloat("musicVolume"); //Load last saved sound volume from player prefs
        currentVolume += _change;

        //Check if we reached the maximum or minimum value
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //Assign final value
        float finalValue = currentVolume * baseVolume;
        musicSource.volume = finalValue;

        //Save final value to player prefs
        PlayerPrefs.SetFloat("musicVolume", currentVolume);
    }










    private void Start()
    {
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            SFXLoad();
        }
        else
        {
            SFXLoad();
        }

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            MusicLoad();
        }
        else
        {
            MusicLoad();
        }
    }
    public void ChangeSFXViaSlider()
    {
        sfxSource.volume = sfxSlider.value;
        SFXSave();
    }

    private void SFXSave()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

    private void SFXLoad()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void ChangeMusicViaSlider()
    {
        musicSource.volume = musicSlider.value;
        MusicSave();
    }

    private void MusicSave()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    private void MusicLoad()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

}
