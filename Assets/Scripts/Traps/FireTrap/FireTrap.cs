using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header("-------Damage Parameters-------")]
    [SerializeField] private float damage;

    [Header("-------FireTrap Timers-------")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("-------Sound Parameters-------")]
    [SerializeField] private AudioClip fireTrapSound;

    private bool triggered;
    private bool active;

    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        triggered = true;
        spriteRend.color = Color.red; //Turn the sprite red to notify the player

        //Wait for delay, activate trap, turn on animation
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //Turn the sprite to normal
        active = true;
        anim.SetBool("activated", true);
        SoundManager.instance.PlaySound(fireTrapSound);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
