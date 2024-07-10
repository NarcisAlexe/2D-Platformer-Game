using UnityEngine;

public class FakeCheckpoint : MonoBehaviour
{
    [SerializeField] private GameObject wallToAppear1;
    [SerializeField] private GameObject wallToAppear2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            wallToAppear1.SetActive(true);
            wallToAppear2.SetActive(true);
        }
    }
}
