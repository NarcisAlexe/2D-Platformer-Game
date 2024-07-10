using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text txt;

    private float elapsedTime;
    public int minutes;
    public int seconds;

    public float fastestTime = 500;
    public int fastestMinutes;
    public int fastestSeconds;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("FastestTime"))
        {
            fastestTime = PlayerPrefs.GetFloat("FastestTime");
            fastestMinutes = Mathf.FloorToInt(fastestTime / 60);
            fastestSeconds = Mathf.FloorToInt(fastestTime % 60);
            Debug.Log("Fastest time loaded: " + fastestTime);
        }
        else
        {
            fastestTime = Mathf.Infinity;
            Debug.Log("Fastest time not found, setting to infinity");
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        minutes = Mathf.FloorToInt(elapsedTime / 60);
        seconds = Mathf.FloorToInt(elapsedTime % 60);
        txt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        CheckFastestTime();
        Debug.Log(elapsedTime);
    }

    public void CheckFastestTime()
    {
        if (elapsedTime < fastestTime)
        {
            fastestTime = elapsedTime;
            fastestMinutes = Mathf.FloorToInt(fastestTime / 60);
            fastestSeconds = Mathf.FloorToInt(fastestTime % 60);
            PlayerPrefs.SetFloat("FastestTime", fastestTime);
        }
    }
}
