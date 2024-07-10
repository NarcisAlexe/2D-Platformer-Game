using UnityEngine;
using UnityEngine.UI;

public class FastestTime : MonoBehaviour
{
    [SerializeField] private Timer timer;
    private Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
    }

    public void ShowFinalTime()
    {
        if (timer != null)
        {
            txt.text = string.Format("{0:00}:{1:00}", timer.fastestMinutes, timer.fastestSeconds);
        }
        else
        {
            Debug.LogWarning("Timer reference is not set!");
        }
    }
}
