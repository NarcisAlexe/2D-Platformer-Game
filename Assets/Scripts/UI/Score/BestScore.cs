using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour
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
            txt.text = scoreManager.bestScore.ToString();
        }
        else
        {
            Debug.LogWarning("ScoreManager reference is not set!");
        }
    }
}
