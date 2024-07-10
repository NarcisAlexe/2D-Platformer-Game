using UnityEngine;

public class CollectebleKey : MonoBehaviour
{
    public bool isPicked { get; private set; }

    [Header ("Discover")]
    [SerializeReference] private GameObject wallToDisappear;
    [SerializeReference] private GameObject Go;

    [Header("Audio Parameters")]
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            gameObject.SetActive(false);
            isPicked = true;
            wallToDisappear.SetActive(false);
            Go.SetActive(true);
        }
        else
        {
            isPicked=false;
            Go.SetActive(false);
        }
    }
}
