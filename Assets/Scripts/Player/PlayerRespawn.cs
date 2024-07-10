using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager manager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        manager = FindObjectOfType<UIManager>();
    }

    private void CheckRespawn()
    {
        //Check if checkpoint available
        if (currentCheckpoint == null)
        {
            //Show game over screen
            manager.GameOver();

            return;
        }

        playerHealth.Respawn();
        transform.position = currentCheckpoint.position;

        //Move the camera to the checkpoint's room
        Camera.main.GetComponent<CameraControl>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
