using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    [Header("Score Parameters")]
    [SerializeField] private ScoreManager scoreManager;

    private Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
    }

    private void Update()
    {
        if (scoreManager != null)
        {
            txt.text = scoreManager.score.ToString();
        }
        else
        {
            Debug.LogWarning("ScoreManager reference is not set!");
        }
    }
}
