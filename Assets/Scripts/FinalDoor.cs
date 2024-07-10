using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    [SerializeField] private GameObject youWonScreen;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private FinalTime finalTime;
    [SerializeField] private FastestTime fastestTime;

    private void Awake()
    {
        youWonScreen.GetComponent<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")    
        {
            SoundManager.instance.PlaySound(winSound);
            youWonScreen.SetActive(true);
            Time.timeScale = 0;
            finalTime.ShowFinalTime();
            fastestTime.ShowFinalTime();
        }
    }
}
