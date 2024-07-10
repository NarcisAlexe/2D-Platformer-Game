using UnityEngine;

public class Cherry : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator anim;

    [Header("Score Parameters")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private int cherryScore = 20;

    [Header("Audio Parameters")]
    [SerializeField] private AudioClip pickUpSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickUpSound);
            anim.SetTrigger("collected");
            scoreManager.CountUp(cherryScore);
        }
    }

    private void DisableCherry()
    {
        gameObject.SetActive(false);
    }
}
