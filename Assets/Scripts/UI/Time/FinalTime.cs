using UnityEngine;
using UnityEngine.UI;

public class FinalTime : MonoBehaviour
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
            txt.text = string.Format("{0:00}:{1:00}", timer.minutes, timer.seconds);
        }
        else
        {
            Debug.LogWarning("Timer reference is not set!");
        }
    }
}
