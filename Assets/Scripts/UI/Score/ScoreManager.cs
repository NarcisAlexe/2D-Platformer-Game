using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private string scoreValue;

    private Text txt;
    public int score = 0;
    public int bestScore;

    private void Awake()
    {
        txt = GetComponent<Text>();
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    public void CountUp(int value)
    {
        score += value;
        txt.text = score.ToString();
        CheckBestScore();
    }

    public void CheckBestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }
}
